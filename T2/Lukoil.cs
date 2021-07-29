using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using T2.Entities;
using T2.Helpers;

namespace T2
{
    public partial class Lukoil : Form
    {
        private Lukoilcl _lukoil;
        public double SUm { get; set; }
       
        private Dictionary<string, int> _lastFoodsCount { get; set; }
        public Lukoil()
        {
            InitializeComponent();
            _lukoil = new Lukoilcl()
            {
                Fuels = Helpers.LukoilHelper.GetFuels(),
                Foods = Helpers.LukoilHelper.GetFoods(),
            };

            _lastFoodsCount = new Dictionary<string, int>();

        }


        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearUserInputs();
            PAybtn.Text = "0.00";
            ChangeButtonsVisibility();
        }
        private void guna2RadioButton2_CheckedChanged(object sender, EventArgs e)
        {

            LiterMStxtB.Enabled = false;

            PriceMStxtB.Enabled = PriceRbtn.Checked;
            LiterMStxtB.Visible = !LiterMStxtB.Visible;
            LiterTxtBx.Visible = !LiterTxtBx.Visible;

            if (!PriceRbtn.Checked)
            {
                PriceMStxtB.Text = String.Empty;
                LiterTxtBx.Text = String.Empty;
            }

        }

        private void FuelList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var fuel = FuelList.SelectedItem as Fuel;
            FuelPricetxt.Text = fuel?.Price.ToString();
        }

        private void LiterRbtn_CheckedChanged(object sender, EventArgs e)
        {
            LiterMStxtB.Enabled = LiterRbtn.Checked;
            if (!LiterRbtn.Checked)
                LiterMStxtB.Text = String.Empty;
        }

        private void PriceMStxtB_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            LiterMStxtB.Text = String.Empty;
        }

        private void LiterMStxtB_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
        }

        private void Lukoil_Load(object sender, EventArgs e)
        {
            FuelList.ValueMember = "Name";

            FuelList.DataSource = _lukoil.Fuels;
            HotDogPriceTxtBx.Text = _lukoil["Hot-Dog"].ToString();
            HamburgerPriceTxtBx.Text = _lukoil["Hamburger"].ToString();
            FriesPriceTxtBx.Text = _lukoil["Fries"].ToString();
            CocaColaPriceTxtBx.Text = _lukoil["Coca-Cola"].ToString();

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void LiterMStxtB_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(LiterMStxtB.Text))
            {
                c.Text = "0.00";
            }
            else
            {
                var cost = Convert.ToDouble(LiterMStxtB.Text) * Convert.ToDouble(FuelPricetxt.Text);
                c.Text = cost.ToString();
            }


        }

        private void PriceMStxtB_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(PriceMStxtB.Text))
            {
                c.Text = "0.00";
                LiterTxtBx.Text = String.Empty;
            }
            else
            {
                var liter = Convert.ToDouble(PriceMStxtB.Text) / Convert.ToDouble(FuelPricetxt.Text);

                LiterTxtBx.Text = liter.ToString("F");
                c.Text = PriceMStxtB.Text;
            }
        }

        private void SaveToFiles(Bill bill)
        {
            var directoryName = @"Bills\";

            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);

            var fileName = Helpers.FileHelper.CreateNewFileName(bill.Guid);


            //save to json file

            Helpers.FileHelper.WriteToJsonFile(bill, $"{directoryName}{fileName}.json");

            var desktopFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            //save to pdf file


            Helpers.FileHelper.WriteToPdf(bill, $@"{desktopFolderPath}\{fileName}.pdf");
        }
        private void ChangeProgramUseability()
        {
            groupBox1.Enabled = !groupBox1.Enabled;
            //groupBox2.Enabled = !groupBox2.Enabled;

        }
        private void ChangeButtonsVisibility()
        {
            PAybtn.Visible = !PAybtn.Visible;
            Clearbtn.Visible = !Clearbtn.Visible;
        }
        private void PayBtn_Click(object sender, EventArgs e)
        {
            if (!CheckOrder())
            {
                MessageBox.Show("There is nothing to calculate!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var newBill = CreateNewBill();
            TotalCostTxtBx.Text = newBill.TotalCost.ToString("F2");
            _lukoil.Bills.Add(newBill);
            SaveToFiles(newBill);

            MessageBox.Show("Calculated. Bill saved to file.", "Info", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            Clearbtn.Visible = true;
            Clearbtn.Enabled = true;
            ChangeProgramUseability();
            ChangeButtonsVisibility();
        }
        private bool CheckOrder()
        {
            var emptyCost = "0.00";

            return !(c.Text == emptyCost && CafeCostTxtBx.Text == emptyCost);
        }
        private void CalculateNewPrice(MaskedTextBox foodCountMsBx, string foodName)
        {
            var totalCost = Convert.ToDouble(CafeCostTxtBx.Text);

            if (String.IsNullOrWhiteSpace(foodCountMsBx.Text))
            {
                totalCost -= _lukoil[foodName] * _lastFoodsCount[foodName];
                CafeCostTxtBx.Text = totalCost.ToString("F2");
                _lastFoodsCount[foodName] = 0;
            }
            else
            {
                var lastCount = 0;

                try
                {
                    lastCount = _lastFoodsCount[foodName];
                }
                catch
                {
                    lastCount = 0;
                }

                var newCount = Convert.ToInt32(foodCountMsBx.Text);

                totalCost -= _lukoil[foodName] * lastCount;


                var foodCost = newCount * _lukoil[foodName];
                
                CafeCostTxtBx.Text = (totalCost + foodCost).ToString("F2");
                SUm = (totalCost + foodCost);
                _lastFoodsCount[foodName] = newCount;
            }
        }
        private FoodItem CreateNewFoodItem(MaskedTextBox foodCountMsBox, string foodName)
        {
            var foodItem = new FoodItem();

            foodItem.Food = new Food()
            {
                Name = foodName,
                Price = _lukoil[foodName],
            };

            foodItem.Count = Convert.ToInt32(foodCountMsBox.Text);

            foodItem.Cost = foodItem.Food.Price * foodItem.Count;

            return foodItem;
        }
        private Bill CreateNewBill()
        {
            var newBill = new Bill();
            var emptyCost = "0.00";

            if (c.Text != emptyCost)
            {
                var fuelItem = new FuelItem();

                fuelItem.Fuel = FuelList.SelectedItem as Fuel;

                fuelItem.Liter = (LiterRbtn.Checked)
                       ? Convert.ToDouble(LiterMStxtB.Text)
                       : Convert.ToDouble(LiterTxtBx.Text);

                fuelItem.Cost = Convert.ToDouble(c.Text);

                newBill.FuelItem = fuelItem;
            }

            if (CafeCostTxtBx.Text != emptyCost)
            {
                var foodItems = new List<FoodItem>();

                if (HotDogChBx.Checked && !string.IsNullOrWhiteSpace(HotDogCountMsBx.Text))
                {
                    foodItems.Add(CreateNewFoodItem(HotDogCountMsBx, "Hot-Dog"));
                }

                if (HamburgerChBx.Checked && !string.IsNullOrWhiteSpace(HamburgerCountMsBx.Text))
                {
                    foodItems.Add(CreateNewFoodItem(HamburgerCountMsBx, "Hamburger"));
                }

                if (FriesChBx.Checked && !string.IsNullOrWhiteSpace(FriesCountMsBx.Text))
                {
                    foodItems.Add(CreateNewFoodItem(FriesCountMsBx, "Fries"));
                }

                if (CocaColaChBx.Checked && !string.IsNullOrWhiteSpace(CocaColaCountMsBx.Text))
                {
                    foodItems.Add(CreateNewFoodItem(CocaColaCountMsBx, "Coca-Cola"));
                }

                newBill.FoodItems = foodItems;
            }

            newBill.TotalCost = newBill.FuelItem.Cost + newBill.FoodItems.Sum(f => f.Cost);
            return newBill;
        }


        private void PayGrpBox_Enter(object sender, EventArgs e)
        {

        }


        private void ClearUserInputs()
        {
            ChangeProgramUseability();

            if (LiterRbtn.Checked)
                LiterRbtn.Checked = !LiterRbtn.Checked;
            else
                PriceRbtn.Checked = !PriceRbtn.Checked;

            if (HotDogChBx.Checked)
                HotDogChBx.Checked = !HotDogChBx.Checked;

            if (HamburgerChBx.Checked)
                HamburgerChBx.Checked = !HamburgerChBx.Checked;

            if (FriesChBx.Checked)
                FriesChBx.Checked = !FriesChBx.Checked;

            if (CocaColaChBx.Checked)
                CocaColaChBx.Checked = !CocaColaChBx.Checked;
        }

        private void Sumbtn_Click(object sender, EventArgs e)
        {
            if (!CheckOrder())
            {
                MessageBox.Show("There is nothing to calculate!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var fuelItem = new FuelItem();
            TotalCostTxtBx.Text = string.Empty;
            fuelItem.Cost = Convert.ToDouble(c.Text);

            fuelItem.Totalcost= fuelItem.Cost;
            double cc = Convert.ToDouble(CafeCostTxtBx.Text) + Convert.ToDouble(c.Text);
             TotalCostTxtBx.Text = Convert.ToString(cc);

        }

        private void HotDogChBx_CheckedChanged(object sender, EventArgs e)
        {
            HotDogCountMsBx.Enabled = HotDogChBx.Checked;

            if (!HotDogChBx.Checked)
            {
                HotDogCountMsBx.Text = String.Empty;
            }
        }

        private void HamburgerChBx_CheckedChanged(object sender, EventArgs e)
        {
            HamburgerCountMsBx.Enabled = HamburgerChBx.Checked;

            if (!HamburgerChBx.Checked)
                HamburgerCountMsBx.Text = String.Empty;
        }

        private void FriesChBx_CheckedChanged(object sender, EventArgs e)
        {
            FriesCountMsBx.Enabled = FriesChBx.Checked;

            if (!FriesChBx.Checked)
                FriesCountMsBx.Text = String.Empty;
        }

        private void CocaColaChBx_CheckedChanged(object sender, EventArgs e)
        {
            CocaColaCountMsBx.Enabled = CocaColaChBx.Checked;

            if (!CocaColaChBx.Checked)
                CocaColaCountMsBx.Text = String.Empty;
        }

        private void HotDogCountMsBx_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void HotDogCountMsBx_TextChanged(object sender, EventArgs e)
        {
            CalculateNewPrice(HotDogCountMsBx, "Hot-Dog");
        }

        private void HamburgerCountMsBx_TextChanged(object sender, EventArgs e)
        {
            CalculateNewPrice(HamburgerCountMsBx, "Hamburger");
        }

        private void FriesCountMsBx_TextChanged(object sender, EventArgs e)
        {
            CalculateNewPrice(FriesCountMsBx, "Fries");
        }

        private void CocaColaCountMsBx_TextChanged(object sender, EventArgs e)
        {
            CalculateNewPrice(CocaColaCountMsBx, "Coca-Cola");
        }
    }
}
