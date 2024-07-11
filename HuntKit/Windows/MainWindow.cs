using System;
using Dalamud.Interface.Windowing;
using ECommons.Automation;
using ECommons.Logging;
using FFXIVClientStructs.FFXIV.Common.Math;
using ImGuiNET;

namespace HuntKit.Windows;

public class MainWindow : Window, IDisposable
{
    private Plugin Plugin;
    // We give this window a hidden ID using ##
    // So that the user will see "My Amazing Window" as window title,
    // but for ImGui the ID is "My Amazing Window##With a hidden ID"
    public MainWindow(Plugin plugin)
        : base("Hunt Kit Main Window##mainWindow    ", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(375, 330),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };

        this.Plugin = plugin;
    }

    public void Dispose() { }

    public override void Draw()
    {
        if (ImGui.Button("Settings")) Plugin.ToggleConfigUI();
        if (ImGui.Button("FindRankA")) Plugin.ToggleFindRankA();
        if (ImGui.Button("SetET")) Plugin.ToggleSetET();
        if (ImGui.Button("Fate Helper")) Plugin.ToggleFateHelper();
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
