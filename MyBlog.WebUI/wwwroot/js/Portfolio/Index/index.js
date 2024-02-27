$('body').on('click', '.opening_modal_delete', function () {
    let id = this.id;
    $("#portfolioDelete").click(function () {
        DeletePortfolio(id);
    });
});

function DeletePortfolio(PortfolioId) {

    $.ajax({
        type: 'POST',
        url: '/Portfolio/DeletePortfolio',
        dataType: 'json',
        data: { PortfolioId: PortfolioId },
        success: function (res) {
            if (!res.isValid) {
                alert(res.errorMessage)
            }
            else {
                MyToast.fire({
                    icon: 'success',
                    title: "deleted successufuly",
                })
                location.reload();
            }
        }
    });
}