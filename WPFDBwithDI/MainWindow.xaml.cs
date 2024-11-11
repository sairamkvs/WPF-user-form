using DataAccessLibrary;
using DataAccessLibrary.Repository;
using ModelLibary;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFDBwithDI.ViewModel;

namespace WPFDBwithDI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
      
    {
        private readonly ViewModelMainWindow viewModelMainWindow;
        
        public ObservableCollection<Users> userslist;
        private IRepository<Users> Userrepository;
        public ObservableCollection<Users> filterUsers;
        private Users Editinguser;
        

        public MainWindow()
        {
            InitializeComponent();
            var dbConfiguration = new DbConfiguration();         
            Userrepository = new Repository(dbConfiguration);
            LoadUsersAsync();
 
        }

        public async Task LoadUsersAsync()
        {
            var users = await Userrepository.GetAll();
            userslist = new ObservableCollection<Users>(users);
            filterUsers = new ObservableCollection<Users>(userslist);
            userDataGrid.ItemsSource = filterUsers;
        }



        private async void subbutton_Click(object sender, RoutedEventArgs e)
        {
           int user_id = int.Parse(idbox.Text);
           string user_name = namebox.Text;
            string group = groupbox.Text;
            string host = hostbox.Text;
            long IP = long.Parse(ipbox.Text);
            if (Editinguser != null)
            {
                
                Editinguser.User_Name = user_name;
                Editinguser.Group = group;
                Editinguser.Host = host;
                Editinguser.IP_Address = IP;

                await Userrepository.Update(Editinguser);
                userDataGrid.Items.Refresh();
                Editinguser = null;
                idbox.Clear();
                namebox.Clear();
                groupbox.Clear();
                hostbox.Clear();
                ipbox.Clear();
                subbutton.Content = "Submit";

            }
            else
            {

                Users users = new Users
                {
                    User_Id = user_id,
                    User_Name = user_name,
                    Group = group,
                    Host = host,
                    IP_Address = IP
                };
                await Userrepository.Add(users);

                userslist.Add(users);
                idbox.Clear();
                namebox.Clear();
                groupbox.Clear();
                hostbox.Clear();
                ipbox.Clear();
            }
           
        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(userDataGrid.SelectedItem is Users SelectedUsers)
                {
                    idbox.IsEnabled = false;
                    Editinguser = SelectedUsers;
                    idbox.Text = SelectedUsers.User_Id.ToString();
                    namebox.Text = SelectedUsers.User_Name;
                    groupbox.Text = SelectedUsers.Group;
                    hostbox.Text = SelectedUsers.Host;
                    ipbox.Text = SelectedUsers.IP_Address.ToString();
                    subbutton.Content = "Update";
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (userDataGrid.SelectedItem is Users SelectedUser)
                {
                    var result = MessageBox.Show($"Are you sure you want to delete {SelectedUser.User_Name}", "Delete Conformation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        Userrepository.Delete(SelectedUser);
                        userslist.Remove(SelectedUser);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }
        }

        private async void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
           await filter();
        }

        public async Task filter()
        {

            try
            {
                string textSearch = Search.Text.ToLower();
                if (string.IsNullOrWhiteSpace(textSearch))
                {
                  filterUsers.Clear();
                    

                    foreach (var urs in userslist)
                    {
                        filterUsers.Add(urs);
                    }
                }
                else
                {
                    var filtered  = userslist.Where(u => u.User_Id.ToString().Contains(textSearch) || u.User_Name.ToLower().Contains(textSearch) || u.Group.Contains(textSearch) || u.Host.Contains(textSearch) || u.IP_Address.ToString().Contains(textSearch) );
                    filterUsers.Clear();
                    foreach (var usr in filtered)
                    {
                        filterUsers.Add(usr);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }
        }
    }
}