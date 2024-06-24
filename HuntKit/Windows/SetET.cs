using System;
using Dalamud.Interface.Windowing;
using ECommons.Automation;
using ECommons.Logging;
using FFXIVClientStructs.FFXIV.Common.Math;
using ImGuiNET;

namespace HuntKit.Windows;

public class SetET : Window, IDisposable
{
    private string _ET = string.Empty;
    public SetET():base("SetEt Window")
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(360, 110),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };
    }

    public override void Draw()
    {
        ImGui.Text($"Currently set ET: {(Plugin.Configuration.ET == string.Empty ? "empty" : Plugin.Configuration.ET)}");
        ImGui.InputTextWithHint("##ET", "ET want to set, must have 3 characters", ref _ET, 8);
        ImGui.SameLine();
        if (ImGui.Button("Save") && _ET.Length >= 3)
        {
            setET(_ET);
        }
        if (ImGui.Button("Reset ET"))
        {
            _ET = string.Empty;
            setET(_ET);
        }
        ImGui.SameLine();
        if (ImGui.Button("Shout ET"))
        {
            shoutET();
        }
    }

    public static void shoutET()
    {
        if (Plugin.Configuration.ET.Length >= 3)
        {
            Chat.Instance.ExecuteCommand($"/sh 在<pos>发现<t>，预定于ET{Plugin.Configuration.ET}开打，请勿抢开跟开脸开");
        }
    }

    public static void setET(string ET)
    {
        if (ET.Length >= 3)
        {
            Plugin.Configuration.ET = ET;
            Plugin.Configuration.Save();
            Plugin.Print($"ET is set to {ET}");
        }
    }

    public void Dispose() { } 
}
