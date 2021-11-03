using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace SkladPi
{
    class Folder
    {

        /// <summary>
        /// Products.
        /// </summary>
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();
        
        /// <summary>
        /// Name of folder.
        /// </summary>
        public string Name{ get; set; }

        /// <summary>
        /// Key of sort.
        /// </summary>
        public int Key { get; set; }

        public Folder(TreeNode node, int key)
        {
            Name = node.Text;
            Key = key;

        }
    }
}
