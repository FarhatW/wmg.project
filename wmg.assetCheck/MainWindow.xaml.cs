using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace wmg.assetCheck
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Populate(string header, string tag, TreeView root, TreeViewItem child, bool isfile)
        {
            var driitem = new TreeViewItem
            {
                Tag = tag,
                Header = header
            };
            driitem.Expanded += _driitem_Expanded;
            if (!isfile)
                driitem.Items.Add(new TreeViewItem());

            if (root != null)
            { root.Items.Add(driitem); checkFolderStructure(driitem.Tag.ToString(), driitem); }
            else { child.Items.Add(driitem); }
        }

        private void _driitem_Expanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;
            if (item.Items.Count != 1 || ((TreeViewItem) item.Items[0]).Header != null) return;
            item.Items.Clear();
            foreach (var dir in Directory.GetDirectories(item.Tag.ToString()))
            {
                var dirinfo = new DirectoryInfo(dir);
                Populate(dirinfo.Name, dirinfo.FullName, null, item, false);
                
            }

            foreach (var dir in Directory.GetFiles(item.Tag.ToString()))
            {
                var dirinfo = new FileInfo(dir);
                Populate(dirinfo.Name, dirinfo.FullName, null, item, true);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var driv in Directory.GetDirectories(@"D:\assetManager"))
            {
             
                    Populate(driv, driv, tree, null, false);
            }
         

        }

        private void checkFolderStructure(string path, TreeViewItem folder)
        {
            if (Directory.Exists(path))
            {
                
         
            var dirrectorys = Directory.GetDirectories(path);
            var preview = false;
            var textures = false;
            var vlp = false;
            foreach (var item in dirrectorys)
            {
                if (!Directory.GetDirectories(item).Any()) continue;
                foreach (var itemFolder in Directory.GetDirectories(item))
                {
                    if (Path.GetFileName(itemFolder) == "previews")
                    {
                        preview = true;
                           
                    }
                    if (Path.GetFileName(itemFolder) == "textures")
                    {
                        textures = true;
                    }
                    if (Path.GetFileName(itemFolder) == "vlp")
                    {
                        vlp = true;
                    }
                }


            }
           
           
            if (preview && textures && vlp)
            {
                folder.Background = Brushes.Green;
            }
            else
            {
                folder.Background = Brushes.Red;
            }

              

            }

        }

    }
}
