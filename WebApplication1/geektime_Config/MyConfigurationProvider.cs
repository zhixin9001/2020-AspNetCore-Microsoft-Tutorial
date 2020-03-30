using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace geektime_Config
{
    class MyConfigurationProvider : ConfigurationProvider
    {
        Timer timer;

        public MyConfigurationProvider() : base()
        {
            timer = new Timer();
            timer.Elapsed += (a, b) => Load(true);
            timer.Interval = 3000;
            timer.Start();
        }

        public override void Load()
        {
            //加载数据
            Load(false);
        }

        void Load(bool reload)
        {
            this.Data["lastTime"] = DateTime.Now.ToString();            
            if (reload)
            {
                base.OnReload();
            }
        }
    }
}
