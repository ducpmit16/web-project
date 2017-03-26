// Paging for Menu Partial
$(document).ready(function () {
    $(document).on("click", "#pagerMenu a[href]", function () {
        $.ajax({
            url: $(this).attr("href"),
            type: "GET",
            cache: false,
            success: function (result) {
                $("#tbl-menu").html(result);
            }
        });
        return false;
    });
});

// Ajax button save when clicked in modal form
function addMenu() {
    $.ajax({
        type: 'get',
        url: urlMenu.urlCreate,
        success: function (data) {
            $("#modal-add-menu").html(data);
        }
    });
}

// Ajax button edit when clicked in modal form
function editMenu(id){
    $.ajax({
        type: 'get',
        url: urlMenu.urlEdit,
        data: {
            id: id
        },
        success: function (data) {
            $("#modal-edit-menu").html(data);
        }
    });
}

// Ajax button delete when clicked in modal form
function deleteMenu(id) {
    $.ajax({
        type: 'get',
        url: urlMenu.urlDelete,
        data: {
            id: id
        },
        success: function (data) {
            $("#modal-delete-menu").html(data);
        }
    });
}

// Ajax button detail in a modal form
function detailMenu(id) {
    $.ajax({
        url: urlMenu.urlDetail,
        type: "post",
        data: {
            menu_Id: id
        },
        success: function (data) {
            $("#modal-detail-menu").html("");
            $("#modal-detail-menu").html(data);
        }
    });
}

// Ajax button when meet event key up onboard
function searchMenu() {
    var keySearch = $("form#search-menu input[type=text]").val().trim();
    $.ajax({
        url: urlMenu.urlSearch,
        type: "post",
        data: {
            nm: keySearch
        },
        success: function (data) {
            $("#tbl-menu").html("");
            $("#tbl-menu").html(data);
        }
    });
}