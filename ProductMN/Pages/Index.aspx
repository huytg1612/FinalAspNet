<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutShare.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ProductMN.Pages.Index" EnableEventValidation="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .active-link {
            color: blue;
        }

        #table td {
            border: 0;
            border-top: 1px solid #f4f4f4;
            white-space: nowrap;
        }

        #table th {
            border: 0;
            border-top: 2px solid #f4f4f4;
            border-bottom: 2px solid #f4f4f4;
        }

        .slider-selection {
            background: #f39c12;
        }
    </style>
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>List products
        <small>Control panel</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">List products</li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-xs-9">
                <div class="box">
                    <div class="box-header">
                        <%--<h3 class="box-title"></h3>--%>
                        <div class="input-group input-group-sm hidden-xs" style="width: 150px">
                            <a href="Details.aspx" class="btn btn-block btn-success" style="width: 100%">Create a new product
                            </a>
                        </div>

                    </div>
                    <div class="box-body table-responsive padding" id="table">
                        <asp:GridView ID="ProductGrid"
                            AutoGenerateColumns="False"
                            EmptyDataText="No data available."
                            runat="server" CssClass="table table-hover" BorderStyle="None">
                            <Columns>
                                <asp:BoundField DataField="ProductName" HeaderText="ProductName" />
                                <asp:BoundField DataField="UnitPrice" HeaderText="UnitPrice" />
                                <asp:BoundField DataField="UnitsInStock" HeaderText="UnitsInStock" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button Text="Delete" runat="server" OnClientClick="return delete_confirm_swal(this);" OnClick="Delete_Click" CommandArgument='<%#Eval("ProductID")%>' CssClass="btn btn-block btn-danger" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <a href="Details.aspx?Id=<%#Eval("ProductID")%>">Edit</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="box-footer clearfix">
                        <ul class="pagination pagination-sm no-margin pull-right" id="pagination">
                            <%--                                <li><a href="#">«</a></li>
                                <li><a href="#">1</a></li>
                                <li><a href="#">2</a></li>
                                <li><a href="#">3</a></li>
                                <li><a href="#">»</a></li>--%>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="col-xs-3">
                <div class="box">
                    <div class="box-header">
                        <h3 class="box-title">Filter</h3>
                    </div>
                    <div class="box-body">
                        <div class="box-tools">
                            <div class="input-group input-group-sm hidden-xs">
                                <asp:Label runat="server" Text="Search name"></asp:Label>
                                <input type="text" id="txtSearch" class="form-control pull-right" />
                                <asp:Label runat="server" Text="Price" CssClass="pull-left"></asp:Label>
                                <input id="price-slider" type="text" class="span2" value="" data-slider-min="1" data-slider-max="10000" data-slider-step="5" data-slider-value="[3000,7000]" />
                                <div class="form-group">
                                    <asp:Label runat="server">Supplier</asp:Label>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="Suppliers">
                                        <%--<option>option 1</option>
                                        <option>option 2</option>
                                        <option>option 3</option>
                                        <option>option 4</option>
                                        <option>option 5</option>--%>
                                    </asp:DropDownList>
                                </div>
                                <button type="button" class="btn btn-block btn-primary" onclick="btn_filter()" style="margin-top: 5px">Filter</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:Label ID="Error_Notice" ForeColor="Red" runat="server">
        </asp:Label>
    </section>
    <script type="text/javascript" src="../bower_components/jquery/dist/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@8"></script>
    <script type="text/javascript" src="../plugins/seiyria-bootstrap-slider/bootstrap-slider.min.js"></script>
    <script type="text/javascript">
        pageInit();
        function pageInit() {
            pagination();
            $("#price-slider").slider({});

            set_default_filter();
        }

        function set_default_filter() {
            var url_string = window.location.href;
            var url = new URL(url_string);
            var search = url.searchParams.get("txtSearch");
            var minPrice = url.searchParams.get("minPrice");
            var maxPrice = url.searchParams.get("maxPrice");
            var supId = url.searchParams.get("supplier_id");

            if (search != null) {
                $('#txtSearch').val(search);
            }
            if (minPrice != null && maxPrice != null) {
                $('#price-slider').val(minPrice + "," + maxPrice);
            }
            if (supId != null) {
                $('#<%= Suppliers.ClientID %>').val(supId);

            }
        }

        function btn_filter() {
            var slider = $('#price-slider');
            var search = $('#txtSearch').val();
            var minPrice = slider.val().split(",")[0];
            var maxPrice = slider.val().split(",")[1];
            var supplier_id = $('#<%= Suppliers.ClientID %>').val();
            //alert(supplier_id);
            var href = "Index.aspx?txtSearch=" + search + "&minPrice=" + minPrice + "&maxPrice=" + maxPrice + "&page=1&supplier_id=" + supplier_id;
            window.location.href = href;
        }

        function delete_confirm_swal(btnDelete) {
            if (btnDelete.dataset.confirmed) {
                btnDelete.dataset.confirmed = false;
                return true;
            } else {
                event.preventDefault();

                Swal.fire({
                    title: 'Message',
                    text: "Do you want to delete?",
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Yes, delete it!'
                }).then((res) => {
                    if (res.value) {
                        btnDelete.dataset.confirmed = true;
                        // Trigger button click programmatically
                        btnDelete.click();
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

        function pagination() {
            var total_count = <%=total_count%>;
            var limit = <%= limit %>;
            var page = <%= page %>;

            //alert(total_count + " " + limit);

            var pagi_num = 2;
            var isDot = true;

            var txtSearch = "<%= _txtSearch %>";
            var minPrice = <%= _minPrice %>;
            var maxPrice = <%= _maxPrice %>;

            var url_string = window.location.href;
            var url = new URL(url_string);
            var supplier_id = url.searchParams.get("supplier_id");
            if (supplier_id == null) {
                supplier_id = "";
            }

            //alert(txtSearch + " " + minPrice + " " + maxPrice);
            var url = "Index.aspx?txtSearch=" + txtSearch + "&minPrice=" + minPrice + "&maxPrice=" + maxPrice + "&supplier_id=" + supplier_id;

            page++;

            var pagi_container = $('#pagination');
            pagi_container.append(
                $('<li>')
                    .append($('<a href=\'' + url + "&page=" + 1 + '\'>').append('First'))
            );
            pagi_container.append(
                $('<li>')
                    .append($('<a href=\'' + url + "&page=" + (page - 1) + '\'>').append('«'))
            );

            for (var i = 1; i <= Math.ceil(total_count / limit); i++) {
                if (page == i) {
                    if (page > 1) {
                        pagi_container.append(
                            $('<li>')
                                .append($('<a href=\'' + url + "&page=" + (i - 1) + '\'>').append(i - 1))
                        );
                    }
                    pagi_container.append(
                        $('<li>')
                            .append($('<a href=\'' + url + "&page=" + i + '\' style="background-color:#3c8dbc;color:white" >').append(i))
                    );
                    isDot = false;
                }
                else if (i > page && i <= page + pagi_num) {
                    pagi_container.append(
                        $('<li>')
                            .append($('<a href=\'' + url + "&page=" + i + '\'>').append(i))
                    );
                }
                else if (i == Math.ceil(total_count / limit)) {
                    pagi_container.append(
                        $('<li>')
                            .append($('<a href=\'' + url + "&page=" + i + '\'>').append(i))
                    );
                } else {
                    if (!isDot) {
                        pagi_container.append(
                            $('<li>')
                                .append($('<a href="#">&#187;').append('...'))
                        );
                        isDot = true;
                    }
                }
                console.log(i);
            }

            pagi_container.append(
                $('<li>')
                    .append($('<a href=\'' + url + "&page=" + (page + 1) + '\'>').append('»'))
            );
            pagi_container.append(
                $('<li>')
                    .append($('<a href=\'' + url + "&page=" + (Math.ceil(total_count / limit)) + '\'>').append('Last'))
            );
        }
    </script>
</asp:Content>

