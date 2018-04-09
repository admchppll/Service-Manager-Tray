using MaterialSkin;
using MaterialSkin.Controls;
using ServiceClassLibrary;
using System.Data;
using System.Windows.Forms;

namespace ServiceManager
{
    public partial class ServiceSelectionForm : MaterialForm
    {
        public ServiceSelectionForm()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

            foreach (Service row in Service.GetAllServices()) {
                serviceSelectList.Items.Add(new ListViewItem(new string[] {row.Name,row.MachineName,row.Description}));
            }
            //serviceSelectList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void serviceSelectList_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }
    }
}
