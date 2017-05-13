using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace Cars
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            displayCars();
        }

        private bool isCarRowSelected()
        {
            if(carDataGridView.SelectedRows.Count > 0)
            {
                return true;
            }
            return false;
        }

        private void displayCars()
        {
            using (CarsDBEntities entities = new CarsDBEntities())
            {
                entities.Cars.Load();
                carDataGridView.DataSource = entities.Cars.Local;
            }
        }

        private void addCar(string name, string colour)
        {
            using (CarsDBEntities entities = new CarsDBEntities())
            {
                Car car = new Car { Name = name, Colour = colour };
                entities.Cars.Add(car);
                entities.SaveChanges();
            }
        }

        private void removeCar()
        {
            int carId = Convert.ToInt32(carDataGridView.SelectedRows[0].Cells[0].Value);

            using (CarsDBEntities entities = new CarsDBEntities())
            {
                if(isCarRowSelected())
                {
                    Car car = (from r in entities.Cars where r.CarsId == carId select r).FirstOrDefault();
                    entities.Cars.Remove(car);
                    entities.SaveChanges();
                }
            }
        }

        private void updateCar(string name, string colour)
        {
            int carId = Convert.ToInt32(carDataGridView.SelectedRows[0].Cells[0].Value);

            using (CarsDBEntities entities = new CarsDBEntities())
            {
                Car car = (from c in entities.Cars where c.CarsId == carId select c).FirstOrDefault();
                car.Name = name;
                car.Colour = colour;
                entities.SaveChanges();
            }
        }

        private void btnAddCar_Click(object sender, EventArgs e)
        {
            addCar(txtName.Text, txtColour.Text);
            displayCars();
        }

        private void btnRemoveCar_Click(object sender, EventArgs e)
        {
            removeCar();
            displayCars();
        }

        private void btnUpdateCar_Click(object sender, EventArgs e)
        {
            updateCar(txtName.Text, txtColour.Text);
            displayCars();
        }

        private void carDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if(isCarRowSelected())
            {
                txtName.Text = carDataGridView.SelectedRows[0].Cells[1].Value.ToString();
                txtColour.Text = carDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            }
        }
    }
}
