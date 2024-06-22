using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using System.IO;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin.Services;
using ECommons;
using HuntKit.Windows;
using Lumina.Excel.GeneratedSheets2;

namespace HuntKit;

public sealed class Plugin : IDalamudPlugin
{
    private const string MainWindowCommand = "/huntkit";
    private const string SetETWindowCommand = "/setet";
    private const string FindRankAWindowCommand = "/ranka";
    private const string shoutET = "/shoutet";
    private DalamudPluginInterface PluginInterface { get; init; }
    private ICommandManager CommandManager { get; init; }
    public static Configuration Configuration { get; set; }

    public readonly WindowSystem WindowSystem = new("HuntKit");
    private ConfigWindow ConfigWindow { get; init; }
    private MainWindow MainWindow { get; init; }
    private FindRankA FindRankA { get; init; }
    private SetET SetET { get; init; }

    public Plugin(
        [RequiredVersion("1.0")] DalamudPluginInterface pluginInterface,
        [RequiredVersion("1.0")] ICommandManager commandManager,
        [RequiredVersion("1.0")] ITextureProvider textureProvider)
    {
        PluginInterface = pluginInterface;
        CommandManager = commandManager;

        ECommonsMain.Init(pluginInterface, this);

        Configuration = PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
        Configuration.Initialize(PluginInterface);

        // you might normally want to embed resources and load them from the manifest stream

        ConfigWindow = new ConfigWindow(this);
        MainWindow = new MainWindow(this);
        FindRankA = new FindRankA(this);
        SetET = new SetET(this);

        WindowSystem.AddWindow(ConfigWindow);
        WindowSystem.AddWindow(MainWindow);
        WindowSystem.AddWindow(FindRankA);
        WindowSystem.AddWindow(SetET);

        CommandManager.AddHandler(SetETWindowCommand, new CommandInfo(OnCommand)
        {
            HelpMessage = "call out set et window"
        });

        CommandManager.AddHandler(MainWindowCommand, new CommandInfo(OnCommand)
        {
            HelpMessage = "call out main window"
        });

        commandManager.AddHandler(FindRankAWindowCommand, new CommandInfo(OnCommand)
        {
            HelpMessage = "call out find rank A mob window"
        });

        commandManager.AddHandler(shoutET, new CommandInfo(OnCommand)
        {
            HelpMessage = "shout the ET"
        });

        PluginInterface.UiBuilder.Draw += DrawUI;

        // This adds a button to the plugin installer entry of this plugin which allows
        // to toggle the display status of the configuration ui
        PluginInterface.UiBuilder.OpenConfigUi += ToggleConfigUI;

        // Adds another button that is doing the same but for the main ui of the plugin
        PluginInterface.UiBuilder.OpenMainUi += ToggleMainUI;
    }

    public void Dispose()
    {
        WindowSystem.RemoveAllWindows();
        ConfigWindow.Dispose();
        MainWindow.Dispose();
        FindRankA.Dispose();
        SetET.Dispose();
        ECommonsMain.Dispose();

        CommandManager.RemoveHandler(MainWindowCommand);
        CommandManager.RemoveHandler(SetETWindowCommand);
        CommandManager.RemoveHandler(FindRankAWindowCommand);
    }

    private void OnCommand(string command, string args)
    {
        // in response to the slash command, just toggle the display status of our main ui
        if (command == MainWindowCommand) ToggleMainUI();
        if (command == SetETWindowCommand) ToggleSetET();
        if (command == FindRankAWindowCommand) ToggleFindRankA();
        if (command == shoutET) SetET.shoutET();
    }

    private void DrawUI() => WindowSystem.Draw();

    public void ToggleConfigUI() => ConfigWindow.Toggle();
    public void ToggleMainUI() => MainWindow.Toggle();
    public void ToggleFindRankA() => FindRankA.Toggle();
    public void ToggleSetET() => SetET.Toggle();
}
