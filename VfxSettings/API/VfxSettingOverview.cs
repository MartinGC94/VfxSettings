using System;
using System.Collections.Generic;
using System.Text;

namespace MartinGC94.VfxSettings.API
{
    public sealed class VfxSettingOverview
    {
        public VfxSetting[] EnabledSettings { get; }
        public VfxSetting[] DisabledSettings { get; }

        internal VfxSettingOverview(List<VfxSetting> enabledSettings, List<VfxSetting> disabledSettings)
        {
            EnabledSettings = enabledSettings.ToArray();
            DisabledSettings = disabledSettings.ToArray();
        }
    }
}
