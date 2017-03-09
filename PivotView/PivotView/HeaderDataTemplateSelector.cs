using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PivotView
{
    public class HeaderDataTemplateSelector : DataTemplateSelector
    {
        private DataTemplate _selectedDataTemplate;
        public DataTemplate SelectedDataTemplate
        {
            get { return _selectedDataTemplate; }
        }
        public DataTemplate NormalDataTemplate
        {
            get { return _normalDataTemplae; }
        }
        private DataTemplate _normalDataTemplae;
        public HeaderDataTemplateSelector()
        {
            _selectedDataTemplate = new DataTemplate(typeof(SelectedDataTempate));
            _normalDataTemplae = new DataTemplate(typeof(NormalDataTemplate));
        }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return _normalDataTemplae;
        }
    }
}
