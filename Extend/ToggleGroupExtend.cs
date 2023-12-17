using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

namespace YFramework
{
    public static class ToggleGroupExtend 
    {
        /// <summary>
        /// 获取当前选择的Toggle
        /// </summary>
        /// <param name="tg"></param>
        /// <returns></returns>
        public static Toggle GetActiveToggle( this ToggleGroup tg ) {
            if (tg == null) return null;
            IEnumerable<Toggle> activeTG = tg.ActiveToggles();
            if (activeTG == null) return null;
            if (activeTG.Count() >= 1) {
                return activeTG.First();
            }
            return null;
        }
    }
}
