using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Collections.Specialized;


namespace lab1._1
{
    class Book
    {
       public string Title{ get; set; }
        public string Author {  get; set; }  
        public string Year { get; set; }
        public string ISBN { get; set; }

    }
    class Library
    {
        public static List<Book> ShowAll()
        {
            var file = @"D:\Library1\list.txt";

            List<Book> books = new List<Book>(); 

            if (File.Exists(file))
            {
                var lines = File.ReadAllLines(file);

                foreach (var line in lines)
                {
                    string[] properties = line.Split(',');
                    Book book = new Book  
                    {
                        Title = properties[0].Trim(),
                        Author = properties[1].Trim(),
                        Year = properties[2].Trim(),
                        ISBN = properties[3].Trim()
                    };

                    books.Add(book); 

                }
            }
            return books;

        }
        public static void Add(string title, string author, string year, string isbn) {
           var file = @"D:\Library1\list.txt";

            using (StreamWriter writer = new StreamWriter(file, append: true))
            {
                writer.WriteLine(title + "," + author + "," + year + "," + isbn);
            }
         
        }

        public void Delete(string book) {
          var file = @"D:\Library1\list.txt";
            List<string> result = new List<string>();
           foreach (var item in File.ReadAllLines(file)) {
                if (!item.Contains(book)) { result.Add(item); }
            }

            File.WriteAllLines(file, result);
        }

        public static List<Book> Search(string value) {

            List<Book> books = new List<Book>();
            var file = @"D:\Library1\list.txt";
          
            foreach (var item in File.ReadAllLines(file))
            {
               
                    string[] properties = item.Split(',');


                    Book book = new Book
                    {
                        Title = properties[0].Trim(),
                        Author = properties[1].Trim(),
                        Year = properties[2].Trim(),
                        ISBN = properties[3].Trim()
                    };
                      if (book.Title ==value || book.Author==value) {
                    books.Add(book); 
                       
                }
            }

            return books;
           
        }    
    }
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

        }
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataGrid1.ItemsSource = null;
           var data= Library.ShowAll();
            DataGrid1.ItemsSource = data;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            string value = TextBoxDelete.Text;
            DataGrid1.ItemsSource = null;
            Library library = new Library();
            library.Delete(value);
            var data = Library.ShowAll(); 
            DataGrid1.ItemsSource = data;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {   
            string value=TextBoxToSearch.Text; 
            DataGrid1.ItemsSource = null;
            var data =Library.Search(value); 
            DataGrid1.ItemsSource = data;

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string title = TextBoxTitle.Text;
            string author = TextBoxAuthor.Text;
            string year = TextBoxYear.Text;
            string isbn = TextBoxisbn.Text;
            Library.Add(title, author, year, isbn);
            var data = Library.ShowAll(); 
            DataGrid1.ItemsSource = data;
        }
    }
    
}