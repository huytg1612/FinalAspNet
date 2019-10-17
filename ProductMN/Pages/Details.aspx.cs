using ProductMN.Domains;
using ProductMN.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProductMN.Pages
{
    public partial class Details : System.Web.UI.Page
    {
        public static int _productId;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    SetDropDownList();
                }

                var idParam = Request["Id"];
                if (idParam == null)
                {
                    Form_Title.Text = "Create a new product";
                    Submit_Btn.Text = "Create";
                }
                else if (!IsPostBack)
                {
                    Form_Title.Text = "Update product";
                    Submit_Btn.Text = "Save";

                    _productId = int.Parse(idParam);

                    SetForm();
                    DropDownListSelected();
                }
            }
            catch (Exception ex)
            {

            }


        }

        public void SetForm()
        {
            using (var ctx = new ProductMNEntities())
            {
                var domain = new ProductDomain(ctx);
                var product = domain.FindById(_productId);

                if (product != null)
                {
                    Product_Id.Text = product.ProductID.ToString();
                    Product_Name.Text = product.ProductName;
                    Unit_Price.Text = product.UnitPrice.ToString();
                    Units_In_Stock.Text = product.UnitsInStock.ToString();
                    Units_On_Order.Text = product.UnitsOnOrder.ToString();
                    Recorder_Level.Text = product.ReorderLevel.ToString();
                    Quantity_Per_Unit.Text = product.QuantityPerUnit;
                }

            }
        }

        public void SetDropDownList()
        {
            using (var ctx = new ProductMNEntities())
            {
                var cateDomain = new CategoryDomain(ctx);
                var supDomain = new SupplierDomain(ctx);

                var suppliers = supDomain.GetAll().Select(s => new DropDownListVMs
                {
                    Text = s.CompanyName,
                    Value = s.SupplierID.ToString()
                }).ToList();

                var categories = cateDomain.GetAll().Select(c => new DropDownListVMs
                {
                    Text = c.CategoryName,
                    Value = c.CategoryID.ToString()
                }).ToList();

                var discontinueds = new List<DropDownListVMs>
                {
                    new DropDownListVMs
                    {
                        Text = "True",
                        Value = "true"
                    },
                    new DropDownListVMs
                    {
                        Text = "False",
                        Value = "false"
                    },
                };

                Suppliers_ID.DataTextField = "Text";
                Suppliers_ID.DataValueField = "Value";
                Suppliers_ID.DataSource = suppliers;
                Suppliers_ID.DataBind();

                Categories_ID.DataTextField = "Text";
                Categories_ID.DataValueField = "Value";
                Categories_ID.DataSource = categories;
                Categories_ID.DataBind();

                Discountinued.DataTextField = "Text";
                Discountinued.DataValueField = "Text";
                Discountinued.DataSource = discontinueds;
                Discountinued.DataBind();
            }
        }
        public void DropDownListSelected()
        {
            using (var ctx = new ProductMNEntities())
            {
                var domain = new ProductDomain(ctx);
                var product = domain.FindById(_productId);

                Categories_ID.ClearSelection();
                Categories_ID.Items.FindByValue(product.CategoryID.ToString()).Selected = true;

                Suppliers_ID.ClearSelection();
                Suppliers_ID.Items.FindByValue(product.SupplierID.ToString()).Selected = true;

                Discountinued.ClearSelection();
                Discountinued.Items.FindByValue(product.Discontinued.ToString()).Selected = true;
            }
        }

        public bool IsFieldValid()
        {
            bool valid = true;

            if (Product_Name.Text.Equals(""))
            {
                Product_Name_Notice.Text = "Product name can't be empty";
                valid = false;
            }
            if (float.Parse(Unit_Price.Text) <= 0)
            {
                Unit_Price_Notice.Text = "Unit price must be >= 0";
                valid = false;
            }
            if (int.Parse(Units_In_Stock.Text) <= 0)
            {
                Unit_In_Stock_Notice.Text = "Unit in stock must be >= 0";
                valid = false;
            }
            if (int.Parse(Units_On_Order.Text) <= 0)
            {
                Unit_On_Order_Notice.Text = "Unit on order must be >= 0";
                valid = false;
            }
            if (int.Parse(Recorder_Level.Text) <= 0)
            {
                Recorder_Level_Notice.Text = "Recorder level must be >= 0";
                valid = false;
            }

            return valid;
        }
        public void Update()
        {
            using (var ctx = new ProductMNEntities())
            {
                var domain = new ProductDomain(ctx);

                var product = new Product();

                product.ProductName = Product_Name.Text;
                product.SupplierID = int.Parse(Suppliers_ID.SelectedValue);
                product.CategoryID = int.Parse(Categories_ID.SelectedValue);
                product.UnitPrice = decimal.Parse(Unit_Price.Text);
                product.UnitsInStock = short.Parse(Units_In_Stock.Text);
                product.UnitsOnOrder = short.Parse(Units_On_Order.Text);
                product.ReorderLevel = short.Parse(Recorder_Level.Text);
                product.QuantityPerUnit = Quantity_Per_Unit.Text;
                product.Discontinued = bool.Parse(Discountinued.SelectedValue);

                domain.Update(_productId, product);
                ctx.SaveChanges();
            }
        }

        public void Create()
        {
            using (var ctx = new ProductMNEntities())
            {
                var domain = new ProductDomain(ctx);

                var product = new Product();

                product.ProductName = Product_Name.Text;
                product.SupplierID = int.Parse(Suppliers_ID.SelectedValue);
                product.CategoryID = int.Parse(Categories_ID.SelectedValue);
                product.UnitPrice = decimal.Parse(Unit_Price.Text);
                product.UnitsInStock = short.Parse(Units_In_Stock.Text);
                product.UnitsOnOrder = short.Parse(Units_On_Order.Text);
                product.ReorderLevel = short.Parse(Recorder_Level.Text);
                product.QuantityPerUnit = Quantity_Per_Unit.Text;
                product.Discontinued = bool.Parse(Discountinued.SelectedValue);

                domain.Create(product);
                ctx.SaveChanges();
            }
        }

        protected void Submit_Btn_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                try
                {
                    if (IsFieldValid())
                    {
                        Button btn = (Button)sender;

                        if (btn.Text.Equals("Create"))
                        {
                            Create();
                            ClientScript.RegisterStartupScript(Page.GetType(), "swal", "success_swal('Create successful');", true);
                        }
                        else if (btn.Text.Equals("Save"))
                        {
                            Update();
                            ClientScript.RegisterStartupScript(Page.GetType(), "swal", "success_swal('Save successful');", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "swal", String.Format("fail_swal('{0}')", ex.Message), true);
                }
            }


        }
    }
}