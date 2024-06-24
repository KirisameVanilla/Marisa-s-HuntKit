using System;
using System.Collections.Generic;
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
    private List<Fate> fateList;
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
        fateList = Svc.Fates.ToList();
        if (ImGui.Button("Stop vnav")) Chat.Instance.ExecuteCommand("/vnav stop");
        foreach (var fate in fateList)
        {
            string fateName = fate.Name.TextValue;
            string fatePos = fate.Position.ToString();

            //get a sub-str like "1, 1, 1" but not like "<1, 1, 1>"
            //the same as [1:fatePos.Length-2]
            fatePos = fatePos[1..^1];
            var posList = fatePos.Split(',');

            ImGui.Text(fateName);
            ImGui.SameLine();

            if (ImGui.Button($"Fly To##{posList[0]}{posList[1]}{posList[2]}"))
            {
                PluginLog.Log($"/vnav flyto {posList[0]}{posList[1]}{posList[2]}");
                Chat.Instance.SendMessage($"/e flyto {fateName}");
                Chat.Instance.ExecuteCommand($"/vnav flyto {posList[0]}{posList[1]}{posList[2]}");
            }

            ImGui.Text(SecondsToTime(fate.TimeRemaining));
            ImGui.SameLine();
            
            ImGui.Text(fate.Progress.ToString() == "0"? string.Empty:$"{fate.Progress}");
        }
    }
    public void Dispose() { }
}
