using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Dalamud.Game.ClientState.Fates;
using Dalamud.Interface.Windowing;
using ECommons.Automation;
using ECommons.DalamudServices;
using ECommons.Logging;
using FFXIVClientStructs.FFXIV.Common.Math;
using ImGuiNET;

namespace HuntKit.Windows;

public class FateHelper : Window, IDisposable
{
    private DateTime startTime = DateTime.MinValue;
    private List<IFate> fateList;
    public FateHelper() : base("Fate List")
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(360, 110),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };
    }

    private static string SecondsToTime(long seconds)
    {
        long minute = seconds / 60;
        long second = seconds % 60;
        return minute >= 0 && second >= 0 ? $"{minute}:" + (second < 10 ? $"0{second}" : $"{second}") : "Need activation.";
    }

    public override void Draw()
    {
        
        fateList = [.. Svc.Fates];
        if (Svc.ClientState.TerritoryType == (ushort)146)
        {
            DrawSouthThanalanTimer(fateList);
        }
        else if (startTime != DateTime.MinValue)
        {
            startTime = DateTime.MinValue;
        }
        
        if (ImGui.Button("Stop vnav")) Chat.Instance.ExecuteCommand("/vnav stop");
        if (ImGui.BeginTable("Fate Table##fate table", 4, ImGuiTableFlags.Resizable))
        {
            ImGui.TableSetupColumn("Fate Name",ImGuiTableColumnFlags.None);
            ImGui.TableSetupColumn("Move to", ImGuiTableColumnFlags.None);
            ImGui.TableSetupColumn("Remaining time",ImGuiTableColumnFlags.None);
            ImGui.TableSetupColumn("Progress",ImGuiTableColumnFlags.None);
            ImGui.TableHeadersRow();

            foreach (var fate in fateList)
            {
                ImGui.TableNextRow();

                //get a sub-str like "1, 1, 1" but not like "<1, 1, 1>"
                //the same as [1:fatePos.Length-2]
                string fatePos = fate.Position.ToString()[1..^1];
                var posList = fatePos.Split(',');

                string fateName = fate.Name.TextValue;
                ImGui.TableNextColumn();
                ImGui.Text(fateName);

                ImGui.TableNextColumn();
                if (ImGui.Button($"Fly To##{posList[0]}{posList[1]}{posList[2]}"))
                {
                    PluginLog.Log($"use vnav to flyto {posList[0]}{posList[1]}{posList[2]}");
                    Chat.Instance.SendMessage($"/e flyto {fateName}");
                    Chat.Instance.ExecuteCommand($"/vnav flyto {posList[0]}{posList[1]}{posList[2]}");
                }

                ImGui.TableNextColumn();
                ImGui.Text(SecondsToTime(fate.TimeRemaining));

                ImGui.TableNextColumn();
                ImGui.Text(fate.Progress.ToString() == "0" ? string.Empty : $"{fate.Progress}");
            }

            ImGui.EndTable();
        }
        
    }

    private void DrawSouthThanalanTimer(List<IFate> _fateList)
    {
        ImGui.Text("This func cannot detect a fate that needs activation failed.\n" +
                   "So if a fate disappears with the column 'Remaining Time' writing 'Need activation', reset the timer manually.");
        DateTime curTime = DateTime.Now;
        if (startTime == DateTime.MinValue) startTime = curTime;
        foreach (var _fate in _fateList)
        {
            if (_fate.State == FateState.Failed) startTime = curTime;
        }

        if (ImGui.Button("Reset timer"))
        {
            startTime = curTime;
        }

        DateTime endTime = startTime.AddHours(1);
        ImGui.Text($"{curTime.ToString("hh:mm:ss tt", CultureInfo.InvariantCulture)}");
        ImGui.Text($"{startTime.ToString("hh:mm:ss tt", CultureInfo.InvariantCulture)} ----> {endTime.ToString("hh:mm:ss tt", CultureInfo.InvariantCulture)}");
    }
    public void Dispose() { }
}
