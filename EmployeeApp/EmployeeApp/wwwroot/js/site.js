https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js

function registerSalary() {
    // Create the success modal dynamically
    var successModal = $('<div class="modal fade" id="successModal" tabindex="-1" role="dialog" aria-labelledby="successModalLabel" aria-hidden="true">\
                            <div class="modal-dialog" role="document">\
                                <div class="modal-content">\
                                    <div class="modal-header">\
                                        <!-- You can customize the header if needed -->\
                                    </div>\
                                    <div class="modal-body text-primary">\
                                        Salary is successfully registered!\
                                    </div>\
                                </div>\
                            </div>\
                        </div>');

    // Append the modal to the body
    $('body').append(successModal);

    // Perform the AJAX request
    $.ajax({
        url: $("#salaryForm").attr("action"),
        type: "POST",
        data: $("#salaryForm").serialize(),
        success: function (data) {
            // Display a success message or perform other actions
            $("#successMessage").text("Salary registered successfully!");

            // Show the dynamically created modal
            successModal.modal("show");

            // Hide the modal after 3 seconds
            setTimeout(function () {
                successModal.modal("hide");
            }, 3000);
        },
        error: function () {
            alert("An error occurred.");
        }
    });
}
/*
catte fonction est responsable pour preand l'information de base de donnees, , 
donc premierment elle communique avec controller (la fonction de Displaylist), 
puis la fonction va envoyer la reponse a end point, ca veaur que la page
MatrixPopup.
*/
function openMatrixButton(identifiant) {
    const displayListUrl = `/MatrixPopup/DisplayList?identifiant=${identifiant}`;

    $.get(displayListUrl, function (data) {
        const popupWindow = window.open('', 'List Popup', 'width=800,height=600');

        if (popupWindow) {
            popupWindow.document.write(data);
        } else {
            console.error('Failed to open pop-up window.');
        }
    }).fail(function (error) {
        console.error('Error fetching DisplayList:', error.responseText);
    });
}



