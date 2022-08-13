using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace TabEX {
    public class TabControlEX : TabControl {
        static TabControlEX() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TabControlEX), new FrameworkPropertyMetadata(typeof(TabControl)));
        }
        public void AddPage(string rHeader, Page rPage, bool rExitable = true) {
            
            if(!UniquePage) {
                foreach(TabItem tie in this.Items) {
                    if(tie.GetType() == rPage.GetType())
                        return;
                    if((tie.Header as string) == rHeader)
                        return;
                }
            }
            
            TabItemEX ti = new TabItemEX() { Header = rHeader, ParentTC = this, Content = new Frame { Content = rPage } };
            ti.Exitable = rExitable;
            Items.Add(ti);
            SelectedValue = ti;
        }

        bool _uniquepage = false;
        public bool UniquePage { get { return _uniquepage; } set { _uniquepage = value; } }
        public static readonly DependencyProperty UniquePageProperty = DependencyProperty.Register("UniquePage", typeof(bool), typeof(TabControlEX), new FrameworkPropertyMetadata(defaultValue: false));
    }
}
