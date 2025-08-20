using MartinGC94.VfxSettings.API;
using MartinGC94.VfxSettings.Native;
using MartinGC94.VfxSettings.Native.User32Enums;
using MartinGC94.VfxSettings.Native.User32Structs;
using System.Collections.Generic;
using System.Management.Automation;
using System.Runtime.InteropServices;

namespace MartinGC94.VfxSettings.Commands
{
    [Cmdlet(VerbsLifecycle.Enable, "VfxSetting")]
    public sealed class EnableVfxSettingCommand : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public VfxSetting[] Setting { get; set; }

        protected override void EndProcessing()
        {
            SetVfxSettings.ToggleVfxSettings(Setting, enable: true, InvokeCommand);
        }
    }

    [Cmdlet(VerbsLifecycle.Disable, "VfxSetting")]
    public sealed class DisableVfxSettingCommand : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public VfxSetting[] Setting { get; set; }

        protected override void EndProcessing()
        {
            SetVfxSettings.ToggleVfxSettings(Setting, enable: false, InvokeCommand);
        }
    }

    internal sealed class SetVfxSettings
    {
        internal static void ToggleVfxSettings(VfxSetting[] settings, bool enable, CommandInvocationIntrinsics cmdInvocationIntrinsics)
        {
            var dwmSettings = new Dictionary<string, uint>();
            var explorerSettings = new Dictionary<string, uint>();
            var personalizeSettings = new Dictionary<string, uint>();
            var apiSettings = new List<VfxSetting>();

            foreach (VfxSetting item in settings)
            {
                switch (item)
                {
                    case VfxSetting.AeroPeek:
                        dwmSettings["EnableAeroPeek"] = (uint)(enable ? 1 : 0);
                        break;

                    case VfxSetting.SaveTaskbarThumbnails:
                        dwmSettings["AlwaysHibernateThumbnails"] = (uint)(enable ? 1 : 0);
                        break;

                    case VfxSetting.AnimateTaskbar:
                        explorerSettings["TaskbarAnimations"] = (uint)(enable ? 1 : 0);
                        break;

                    case VfxSetting.IconShadows:
                        explorerSettings["ListviewShadow"] = (uint)(enable ? 1 : 0);
                        break;

                    case VfxSetting.TranslucentSelection:
                        explorerSettings["ListviewAlphaSelect"] = (uint)(enable ? 1 : 0);
                        break;

                    case VfxSetting.Thumbnails:
                        explorerSettings["IconsOnly"] = (uint)(enable ? 0 : 1);
                        break;

                    case VfxSetting.Transparency:
                        personalizeSettings.Add("EnableTransparency", (uint)(enable ? 1 : 0));
                        break;

                    default:
                        apiSettings.Add(item);
                        break;
                }
            }

            if (dwmSettings.Count > 0)
            {
                Utils.SetRegistrySettings(cmdInvocationIntrinsics, Utils.dwmKey, dwmSettings);
            }

            if (explorerSettings.Count > 0)
            {
                Utils.SetRegistrySettings(cmdInvocationIntrinsics, Utils.explorerAdvancedKey, explorerSettings);
            }

            if (personalizeSettings.Count > 0)
            {
                Utils.SetRegistrySettings(cmdInvocationIntrinsics, Utils.personalizeKey, personalizeSettings);
            }

            if (apiSettings.Count > 0)
            {
                foreach (VfxSetting setting in apiSettings)
                {
                    switch (setting)
                    {
                        case VfxSetting.AnimateInsideWindows:
                            SettingsHandler.SetFeatureToggle(SystemParametersAction.SPI_SETCLIENTAREAANIMATION, enable);
                            break;
                        case VfxSetting.AnimateMinimizeAndMaximize:
                            ANIMATIONINFO animStruct = new ANIMATIONINFO
                            {
                                iMinAnimate = enable ? 1 : 0
                            };
                            animStruct.cbSize = (uint)Marshal.SizeOf(animStruct);
                            NativeMethods.SystemParametersInfoW(SystemParametersAction.SPI_SETANIMATION, animStruct.cbSize, ref animStruct, UpdateFlags.SaveAndPublish);
                            break;
                        case VfxSetting.AnimateMenus:
                            SettingsHandler.SetFeatureToggle(SystemParametersAction.SPI_SETMENUANIMATION, enable);
                            break;
                        case VfxSetting.AnimateTooltips:
                            SettingsHandler.SetFeatureToggle(SystemParametersAction.SPI_SETTOOLTIPANIMATION, enable);
                            break;
                        case VfxSetting.MenuSelectionFade:
                            SettingsHandler.SetFeatureToggle(SystemParametersAction.SPI_SETSELECTIONFADE, enable);
                            break;
                        case VfxSetting.MouseShadow:
                            SettingsHandler.SetFeatureToggle(SystemParametersAction.SPI_SETCURSORSHADOW, enable);
                            break;
                        case VfxSetting.WindowShadow:
                            SettingsHandler.SetFeatureToggle(SystemParametersAction.SPI_SETDROPSHADOW, enable);
                            break;
                        case VfxSetting.ShowWindowContentWhileDragging:
                            SettingsHandler.SetFeatureToggle(SystemParametersAction.SPI_SETDRAGFULLWINDOWS, enable, uiParam: true);
                            break;
                        case VfxSetting.SlideOpenComboBoxes:
                            SettingsHandler.SetFeatureToggle(SystemParametersAction.SPI_SETCOMBOBOXANIMATION, enable);
                            break;
                        case VfxSetting.FontSmoothing:
                            SettingsHandler.SetFeatureToggle(SystemParametersAction.SPI_SETFONTSMOOTHING, enable, uiParam: true);
                            break;
                        case VfxSetting.SmoothScrollListBoxes:
                            SettingsHandler.SetFeatureToggle(SystemParametersAction.SPI_SETLISTBOXSMOOTHSCROLLING, enable);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}