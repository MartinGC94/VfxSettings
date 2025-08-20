using MartinGC94.VfxSettings.API;
using MartinGC94.VfxSettings.Native;
using MartinGC94.VfxSettings.Native.User32Enums;
using MartinGC94.VfxSettings.Native.User32Structs;
using System.Collections.Generic;
using System.ComponentModel;
using System.Management.Automation;
using System.Runtime.InteropServices;

namespace MartinGC94.VfxSettings.Commands
{
    [OutputType(typeof(VfxSettingOverview))]
    [Cmdlet(VerbsCommon.Get, "VfxSetting")]
    public sealed class GetVfxSettingCommand : PSCmdlet
    {
        private readonly List<VfxSetting> enabledSettings = new List<VfxSetting>();
        private readonly List<VfxSetting> disabledSettings = new List<VfxSetting>();
        protected override void EndProcessing()
        {
            PSObject dwmSettings = Utils.GetRegistrySettings(InvokeCommand, Utils.dwmKey);
            if (dwmSettings != null)
            {
                AddSetting(IsSet(dwmSettings, "EnableAeroPeek"), VfxSetting.AeroPeek);
                AddSetting(IsSet(dwmSettings, "AlwaysHibernateThumbnails"), VfxSetting.SaveTaskbarThumbnails);
            }

            PSObject explorerSettings = Utils.GetRegistrySettings(InvokeCommand, Utils.explorerAdvancedKey);
            if (explorerSettings != null)
            {
                AddSetting(IsSet(explorerSettings, "TaskbarAnimations"), VfxSetting.AnimateTaskbar);
                AddSetting(IsSet(explorerSettings, "ListviewShadow"), VfxSetting.IconShadows);
                AddSetting(IsSet(explorerSettings, "ListviewAlphaSelect"), VfxSetting.TranslucentSelection);
                AddSetting(IsSet(explorerSettings, "IconsOnly", expectedValue: 0), VfxSetting.Thumbnails);
            }

            PSObject personalizeSettings = Utils.GetRegistrySettings(InvokeCommand, Utils.personalizeKey);
            if (personalizeSettings != null)
            {
                AddSetting(IsSet(personalizeSettings, "EnableTransparency"), VfxSetting.Transparency);
            }

            var features = new Dictionary<VfxSetting, SystemParametersAction>
            {
               {VfxSetting.AnimateInsideWindows, SystemParametersAction.SPI_GETCLIENTAREAANIMATION },
               {VfxSetting.AnimateMenus, SystemParametersAction.SPI_GETMENUANIMATION },
               {VfxSetting.AnimateTooltips, SystemParametersAction.SPI_GETTOOLTIPANIMATION },
               {VfxSetting.MenuSelectionFade, SystemParametersAction.SPI_GETSELECTIONFADE },
               {VfxSetting.MouseShadow, SystemParametersAction.SPI_GETCURSORSHADOW },
               {VfxSetting.WindowShadow, SystemParametersAction.SPI_GETDROPSHADOW },
               {VfxSetting.ShowWindowContentWhileDragging, SystemParametersAction.SPI_GETDRAGFULLWINDOWS },
               {VfxSetting.SlideOpenComboBoxes, SystemParametersAction.SPI_GETCOMBOBOXANIMATION },
               {VfxSetting.FontSmoothing, SystemParametersAction.SPI_GETFONTSMOOTHING },
               {VfxSetting.SmoothScrollListBoxes, SystemParametersAction.SPI_GETLISTBOXSMOOTHSCROLLING }
            };

            foreach (VfxSetting setting in features.Keys)
            {
                bool value;
                try
                {
                    value = SettingsHandler.GetFeatureToggle(features[setting]);
                }
                catch (Win32Exception exception)
                {
                    WriteError(new ErrorRecord(exception, "APIError", Utils.GetErrorCategory(exception), setting));
                    continue;
                }

                AddSetting(value, setting);
            }

            ANIMATIONINFO animStruct = new ANIMATIONINFO();
            animStruct.cbSize = (uint)Marshal.SizeOf(animStruct);
            try
            {
                SettingsHandler.ThrowOnError(NativeMethods.SystemParametersInfoW(SystemParametersAction.SPI_GETANIMATION, animStruct.cbSize, ref animStruct, UpdateFlags.DoNotPublish));
            }
            catch (Win32Exception exception)
            {
                WriteError(new ErrorRecord(exception, "APIError", Utils.GetErrorCategory(exception), VfxSetting.AnimateMinimizeAndMaximize));
            }

            AddSetting(animStruct.iMinAnimate != 0, VfxSetting.AnimateMinimizeAndMaximize);

            enabledSettings.Sort();
            disabledSettings.Sort();

            VfxSettingOverview result = new VfxSettingOverview(enabledSettings, disabledSettings);
            WriteObject(result);
        }

        private void AddSetting(bool enabled, VfxSetting setting)
        {
            if (enabled)
            {
                enabledSettings.Add(setting);
            }
            else
            {
                disabledSettings.Add(setting);
            }
        }

        private bool IsSet(PSObject regInfo, string property, int expectedValue = 1)
        {
            PSPropertyInfo propInfo = regInfo.Properties[property];
            return propInfo != null && propInfo.Value is int intValue && intValue == expectedValue;
        }
    }
}
