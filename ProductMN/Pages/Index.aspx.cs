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
    public partial class Index : System.Web.UI.Page
    {
        public int total_count = 0;
        public int limit = 5;
        public int page = 1;

        public string _txtSearch = "";
        public decimal? _minPrice = 0;
        public decimal? _maxPrice = 10000;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["txtSearch"] != null && Request["minPrice"] != null && Request["maxPrice"] != null && Request["supplier_id"] != null)
                {
                    var name = Request["txtSearch"];
                    var minPrice = Request["minPrice"];
                    var maxPrice = Request["maxPrice"];
                    var pageParam = Request["page"];
                    var supIdParam = Request["supplier_id"];

                    //txtSearch.Text = name;
                    //this.minPrice.Text = minPrice;
                    //this.maxPrice.Text = maxPrice;

                    try
                    {
                        decimal? min_price = minPrice.Equals("") ? 0 : decimal.Parse(minPrice);
                        decimal? max_price = maxPrice.Equals("") ? 10000 : decimal.Parse(maxPrice);
                        int page = pageParam.Equals("") ? 1 : int.Parse(pageParam);
                        int? supId = supIdParam.Equals("") ? -1 : int.Parse(supIdParam);

                        _txtSearch = name;
                        _minPrice = min_price;
                        _maxPrice = max_price;

                        if (page < 1) page = 1;

                        Search(name, min_price, max_price, page - 1, limit,supId == -1 ? null : supId);
                    }
                    catch (Exception ex)
                    {
                        Error_Notice.Text = ex.Message;
                    }

                }
                else
                {
                    Search("", _minPrice, _maxPrice, 0, limit,null);
                }

                SetDropDownList();
            }
        }

        //public void LoadGrid()
        //{
        //    using (var ctx = new ProductMNEntities())
        //    {
        //        var domain = new ProductDomain(ctx);
        //        var list = domain.GetAll().ToList();

        //        ProductGrid.DataSource = list;
        //        ProductGrid.DataBind();
        //    }
        //}

        public void SetDropDownList()
        {
            using (var ctx = new ProductMNEntities())
            {
                var query = ctx.Suppliers.Select(s => new DropDownListVMs()
                {
                    Text = s.CompanyName,
                    Value = s.SupplierID.ToString()
                }).ToList();

                query.Insert(0,new DropDownListVMs()
                {
                    Text = "---Any---",
                    Value = ""
                });

                Suppliers.DataTextField = "Text";
                Suppliers.DataValueField = "Value";
                Suppliers.DataSource = query;
                Suppliers.DataBind();

            }
        }
        public void Search(string name, decimal? min_price, decimal? max_price, int page, int take,int? supplierId)
        {
            using (var ctx = new ProductMNEntities())
            {
                var domain = new ProductDomain(ctx);
                var model = domain.QueryFilter(name, min_price, max_price, page, take,supplierId);

                ProductGrid.DataSource = model.list;
                ProductGrid.DataBind();

                total_count = model.total_count;
                this.page = page;

                //LiteralControl pagination_elements = new LiteralControl();
                //var pagination = Request.Form["pagination"];

                //Console.WriteLine(pagination);
                //if (pagination != null)
                //{
                //    pagination_elements.Text += @"<li><a href='#'>1</a></li>";
                //    pagination.Controls.Add(pagination_elements);
                //}
            }
        }

        public void Delete(int id)
        {
            using (var ctx = new ProductMNEntities())
            {
                var domain = new ProductDomain(ctx);
                var product = domain.FindById(id);

                if (product != null)
                {
                    domain.Delete(product);
                    ctx.SaveChanges();
                }
            }
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                try
                {
                    Button btn = (Button)sender;
                    var productId = int.Parse(btn.CommandArgument.ToString());

                    Delete(productId);
                    ClientScript.RegisterStartupScript(Page.GetType(), "swal", "success_swal('Delete successful');", true);

                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "swal", String.Format("fail_swal('{0}')", ex.Message), true);
                    Error_Notice.Text = ex.Message;
                }
                if (ClientScript.IsStartupScriptRegistered(Page.GetType(), "swal"))
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "swal", "", true);
                }
            }

        }
    }
}