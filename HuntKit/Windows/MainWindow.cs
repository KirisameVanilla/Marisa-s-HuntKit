using System;
using Dalamud.Interface.Windowing;
using ECommons.Automation;
using ECommons.Logging;
using FFXIVClientStructs.FFXIV.Common.Math;
using ImGuiNET;

namespace HuntKit.Windows;

public class MainWindow : Window, IDisposable
{
    private Plugin plugin;
    public MainWindow(Plugin plugin)
        : base("Hunt Kit Main Window##mainWindow    ", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(375, 330),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };

        this.plugin = plugin;
    }

    public void Dispose() { }

    public override void Draw()
    {
        if (ImGui.Button("Settings")) plugin.ToggleConfigUI();
        if (ImGui.Button("FindRankA")) plugin.ToggleFindRankA();
        if (ImGui.Button("SetET")) plugin.ToggleSetET();
        if (ImGui.Button("Fate Helper")) plugin.ToggleFateHelper();
        if (ImGui.Button("Test ECommons-Chat"))
        {
            try
            {
                Chat.Instance.ExecuteCommand("/e success!<se.1>");
            }
            catch (Exception e)
            {
                PluginLog.Error("Cannot use ECommons-Chat." + e);
                throw;
            }
        }
    }
}
