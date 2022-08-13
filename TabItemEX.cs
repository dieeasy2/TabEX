using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TabEX {
	public class TabItemEX : TabItem {
		static TabItemEX() {
			DefaultStyleKeyProperty.OverrideMetadata(typeof(TabItemEX), new FrameworkPropertyMetadata(typeof(TabItemEX)));
		}
		public TabControlEX? ParentTC { get; set; } = null;
		
		bool _exitable  = true;
		public bool Exitable {
			get { return _exitable; } 
			set { 
				_exitable = value;
				OnApplyTemplate();
			} 
		}
		
		
		public override void OnApplyTemplate() {
			Button? b = this.Template.FindName("Button_Close", this) as Button;
			if (b != null)  {
				b.Click += Button_Click;

				if(_exitable) {
					b.Visibility = Visibility.Visible;
				}
				else {
					b.Visibility = Visibility.Collapsed;
				}
			}
			
		}
		public void Button_Click(object sender, EventArgs e) {
			Button? b = sender as Button;
			if(b == null) return;

			TabItemEX? t = b.Tag as TabItemEX;
			if(t == null) return;


			List<TabItemEX> TI_Delete_List = new List<TabItemEX>();
			
			TabControlEX? TC_Parent = t.ParentTC;
			if (TC_Parent == null) return;


			foreach(TabItemEX it in TC_Parent.Items) {
				Frame? itf = it.Content as Frame;
				if (itf == null) continue;
				if(itf.Content == this) {
					TI_Delete_List.Add(it);
				}
			}

			foreach(TabItemEX it in TI_Delete_List) {
				TC_Parent.Items.Remove(it);
			}

			TC_Parent.Items.Remove(this);
		}
		
	}
}
