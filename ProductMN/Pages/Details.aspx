<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutShare.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="ProductMN.Pages.Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>Product
        <small>Preview</small>
        </h1>
        <section class="content">
            <div class="row">
                <div class="col-md-6">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <asp:Label runat="server" ID="Form_Title" class="box-title"></asp:Label>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div role="form">
                            <div class="box-body">
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Product id</label>
                                    <asp:TextBox type="text" runat="server" class="form-control" ID="Product_Id" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Product name</label>
                                    <asp:TextBox type="text" runat="server" class="form-control" placeholder="Enter product name" ID="Product_Name"></asp:TextBox>
                                    <asp:Label ID="Product_Name_Notice" runat="server" ForeColor="Red"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Quantity per unit</label>
                                    <asp:TextBox type="text" runat="server" class="form-control" placeholder="Enter quantity per unit" ID="Quantity_Per_Unit"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>Suppliers</label>
                                    <asp:DropDownList runat="server" class="form-control" ID="Suppliers_ID">
                                        <asp:ListItem>option 1</asp:ListItem>
                                        <asp:ListItem>option 1</asp:ListItem>
                                        <asp:ListItem>option 1</asp:ListItem>
                                        <asp:ListItem>option 1</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>Categories</label>
                                    <asp:DropDownList runat="server" class="form-control" ID="Categories_ID">
                                        <asp:ListItem>option 1</asp:ListItem>
                                        <asp:ListItem>option 1</asp:ListItem>
                                        <asp:ListItem>option 1</asp:ListItem>
                                        <asp:ListItem>option 1</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>Discontinued</label>
                                    <asp:DropDownList runat="server" class="form-control" ID="Discountinued">
                                        <asp:ListItem>option 1</asp:ListItem>
                                        <asp:ListItem>option 1</asp:ListItem>
                                        <asp:ListItem>option 1</asp:ListItem>
                                        <asp:ListItem>option 1</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Unit price</label>
                                    <asp:TextBox type="number" runat="server" class="form-control" placeholder="Enter unit price" ID="Unit_Price"></asp:TextBox>
                                    <asp:Label ID="Unit_Price_Notice" runat="server" ForeColor="Red"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Units in stock</label>
                                    <asp:TextBox type="number" runat="server" class="form-control" placeholder="Enter units in stock" ID="Units_In_Stock"></asp:TextBox>
                                    <asp:Label ID="Unit_In_Stock_Notice" runat="server" ForeColor="Red"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Units on order</label>
                                    <asp:TextBox type="number" runat="server" class="form-control" placeholder="Enter units on order" ID="Units_On_Order"></asp:TextBox>
                                    <asp:Label ID="Unit_On_Order_Notice" runat="server" ForeColor="Red"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Recorder level</label>
                                    <asp:TextBox type="number" runat="server" class="form-control" placeholder="Enter recorder level" ID="Recorder_Level"></asp:TextBox>
                                    <asp:Label ID="Recorder_Level_Notice" runat="server" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                            <!-- /.box-body -->

                            <div class="box-footer">
                                <asp:Button runat="server" class="btn btn-primary" ID="Submit_Btn" OnClick="Submit_Btn_Click" OnClientClick="return submit_confirm_swal(this);"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </section>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@8"></script>
    <script type="text/javascript">
        function submit_confirm_swal(btnSubmit) {
            if (btnSubmit.dataset.confirmed) {
                btnSubmit.dataset.confirmed = false;
                return true;
            } else {
                event.preventDefault();

                Swal.fire({
                    title: 'Message',
                    text: "Do you want to " + btnSubmit.value + "?",
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Yes, '+btnSubmit.value+' it!'
                }).then((res) => {
                    if (res.value) {
                        btnSubmit.dataset.confirmed = true;
                        // Trigger button click programmatically
                        btnSubmit.click();
                    }
                });
            }
            
        }

        async function success_swal(msg) {
            await Swal.fire({
                title: "Message",
                text: msg,
                type: 'success',
            });

            var href = window.location.href;
            window.location.href = href;
        }

        async function fail_swal(msg) {
            await Swal.fire({
                type: 'error',
                title: 'Message',
                text: msg,
            })
            var href = window.location.href;
            window.location.href = href;
        }
    </script>
</asp:Content>

