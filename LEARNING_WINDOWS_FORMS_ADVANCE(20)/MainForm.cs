using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LEARNING_WINDOWS_FORMS_ADVANCE_20_
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            List<ToolStripMenuItem> allItems =
                new List<ToolStripMenuItem>();

            //این حلقه  تمام آیتمهای منو رو پیمایش می کند و درون یک لیست ذخیره می کند
            foreach (ToolStripMenuItem toolItem in menuStrip.Items)
            {
                allItems.Add(toolItem);

                //add sub items
                allItems.AddRange(GetItems(toolItem));
            }

            //این قسمت آیتمهای منو را بر اساس وضعیت دسترسی فعال و غیر فعال می کند
            if (Temp.MenuList.Count > 0)
            {
                foreach (MenuStatus item in Temp.MenuList)
                {
                    foreach (ToolStripMenuItem menuItem in allItems)
                    {
                        int menuId =
                            int.Parse(menuItem.Tag.ToString());

                        if (menuId == item.MenuID)
                        {
                            menuItem.Enabled = item.MenuIsActive;
                        }
                    }
                }
            }
        }

        private IEnumerable<ToolStripMenuItem> GetItems(ToolStripMenuItem item)
        {
            foreach (ToolStripMenuItem dropDownItem in item.DropDownItems)
            {
                if (dropDownItem.HasDropDownItems)
                {
                    foreach (ToolStripMenuItem subItem in GetItems(dropDownItem))
                    {
                        yield return subItem;
                    }
                }

                yield return dropDownItem;
            }
        }
    }
}
