let editingLocationId = null;

function showLocationForm() {
     document.getElementById('locationForm').classList.remove('d-none');
     document.getElementById('locationName').focus();

     // Only reset form if it's not an edit operation
     if (!editingLocationId) {
          // Reset form for new location
          document.getElementById('locationId').value = '0';
          document.getElementById('locationForm').action = '/Location/AddLocation';
          document.getElementById('submitLocationBtn').textContent = 'Adaugă Locația';

          // Remove edit mode indicator if it exists
          const editModeInput = document.getElementById('editMode');
          if (editModeInput) {
               editModeInput.remove();
          }
     }
}

function hideLocationForm() {
     document.getElementById('locationForm').classList.add('d-none');
     clearLocationForm();
     editingLocationId = null;

     // Reset form to add mode
     document.getElementById('locationForm').action = '/Location/AddLocation';
     document.getElementById('submitLocationBtn').textContent = 'Adaugă Locația';

     // Remove edit mode indicator
     const editModeInput = document.getElementById('editMode');
     if (editModeInput) {
          editModeInput.remove();
     }
}

function clearLocationForm() {
     document.getElementById('locationId').value = '0';
     document.getElementById('locationName').value = '';
     document.getElementById('locationDescription').value = '';
     document.getElementById('locationAddress').value = '';
     document.getElementById('locationPhone').value = '';
     document.getElementById('locationImageFile').value = '';
     document.getElementById('imagePreview').classList.add('d-none');

     // Remove validation classes
     document.querySelectorAll('#locationForm .is-invalid').forEach(field => {
          field.classList.remove('is-invalid');
     });

     // Remove edit mode indicator
     const editModeInput = document.getElementById('editMode');
     if (editModeInput) {
          editModeInput.remove();
     }
}

function previewLocationImage(input) {
     if (input.files && input.files[0]) {
          const reader = new FileReader();

          reader.onload = function (e) {
               document.getElementById('previewImg').src = e.target.result;
               document.getElementById('imagePreview').classList.remove('d-none');
          };

          reader.readAsDataURL(input.files[0]);
     }
}

function editLocation(locationId) {
     editingLocationId = locationId;

     // Get location data via AJAX
     fetch(`/Location/GetLocation?locationId=${locationId}`, {
          method: 'GET',
          headers: {
               'Content-Type': 'application/json'
          }
     })
          .then(response => response.json())
          .then(data => {
               if (data.success) {
                    // Clear form first
                    clearLocationForm();

                    // Populate form with location data
                    document.getElementById('locationId').value = data.data.id;
                    document.getElementById('locationName').value = data.data.name;
                    document.getElementById('locationDescription').value = data.data.description || '';
                    document.getElementById('locationAddress').value = data.data.address;
                    document.getElementById('locationPhone').value = data.data.phone;

                    // Show existing image if available
                    if (data.data.imagePath) {
                         document.getElementById('previewImg').src = data.data.imagePath;
                         document.getElementById('imagePreview').classList.remove('d-none');
                    }

                    // Update form for editing
                    const form = document.getElementById('locationForm');
                    form.action = '/Location/UpdateLocation';
                    form.method = 'POST';

                    // Update submit button
                    const submitBtn = document.getElementById('submitLocationBtn');
                    submitBtn.textContent = 'Actualizează Locația';
                    submitBtn.type = 'submit';

                    // Add hidden field to track that this is an edit
                    let editModeInput = document.getElementById('editMode');
                    if (!editModeInput) {
                         editModeInput = document.createElement('input');
                         editModeInput.type = 'hidden';
                         editModeInput.id = 'editMode';
                         editModeInput.name = 'editMode';
                         form.appendChild(editModeInput);
                    }
                    editModeInput.value = 'true';

                    // Show form
                    document.getElementById('locationForm').classList.remove('d-none');
                    document.getElementById('locationName').focus();

               } else {
                    showNotification('error', data.message || 'Nu s-a putut încărca locația.');
               }
          })
          .catch(error => {
               console.error('Error:', error);
               showNotification('error', 'A apărut o eroare la încărcarea locației.');
          });
}

function deleteLocation(locationId) {
     if (confirm('Sigur doriți să ștergeți această locație? Această acțiune este ireversibilă!')) {
          const formData = new FormData();
          formData.append('locationId', locationId);

          // Add CSRF token
          const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
          formData.append('__RequestVerificationToken', token);

          fetch('/Location/DeleteLocation', {
               method: 'POST',
               body: formData
          })
               .then(response => response.json())
               .then(data => {
                    if (data.success) {
                         // Remove location card from DOM
                         const locationCard = document.querySelector(`[data-location-id="${locationId}"]`);
                         if (locationCard) {
                              locationCard.remove();
                         }

                         // Check if there are no more locations
                         const locationList = document.getElementById('locationList');
                         if (locationList.children.length === 0) {
                              locationList.innerHTML = `
                        <div id="emptyState" class="col-12 text-center py-5">
                            <i class="bi bi-geo-alt" style="font-size: 3rem; color: #6c757d;"></i>
                            <p class="mt-3 text-muted">Nu există locații adăugate momentan.</p>
                        </div>
                    `;
                         }

                         showNotification('success', data.message);
                    } else {
                         showNotification('error', data.message);
                    }
               })
               .catch(error => {
                    console.error('Error:', error);
                    showNotification('error', 'A apărut o eroare la ștergerea locației.');
               });
     }
}

function showNotification(type, message) {
     // Remove existing notifications
     const existingNotifications = document.querySelectorAll('.notification-alert');
     existingNotifications.forEach(notification => notification.remove());

     // Create notification
     const notification = document.createElement('div');
     notification.className = `alert alert-${type === 'success' ? 'success' : 'danger'} alert-dismissible fade show notification-alert`;
     notification.style.position = 'fixed';
     notification.style.top = '20px';
     notification.style.right = '20px';
     notification.style.zIndex = '9999';
     notification.style.minWidth = '300px';

     notification.innerHTML = `
        ${message}
        <button type="button" class="btn-close" onclick="this.parentElement.remove()"></button>
    `;

     document.body.appendChild(notification);

     // Auto remove after 5 seconds
     setTimeout(() => {
          if (notification.parentElement) {
               notification.remove();
          }
     }, 5000);
}

// Form validation and submission handling
document.addEventListener('DOMContentLoaded', function () {
     const locationForm = document.getElementById('locationForm');

     if (locationForm) {
          locationForm.addEventListener('submit', function (e) {
               // Basic client-side validation
               const requiredFields = ['locationName', 'locationDescription', 'locationAddress', 'locationPhone'];
               let isValid = true;

               requiredFields.forEach(fieldId => {
                    const field = document.getElementById(fieldId);
                    if (!field.value.trim()) {
                         field.classList.add('is-invalid');
                         isValid = false;
                    } else {
                         field.classList.remove('is-invalid');
                    }
               });

               // Validate phone format (simple validation)
               const phoneField = document.getElementById('locationPhone');
               const phoneRegex = /^[0-9\+\-\s\(\)]+$/;
               if (phoneField.value && !phoneRegex.test(phoneField.value)) {
                    phoneField.classList.add('is-invalid');
                    showNotification('error', 'Formatul telefonului nu este valid.');
                    isValid = false;
               }

               if (!isValid) {
                    e.preventDefault();
                    showNotification('error', 'Vă rugăm să completați corect toate câmpurile obligatorii.');
                    return false;
               }

               // Log form submission for debugging
               const formAction = locationForm.action;
               const locationIdValue = document.getElementById('locationId').value;
               console.log('Form submission:', {
                    action: formAction,
                    locationId: locationIdValue,
                    isEdit: locationIdValue !== '0'
               });

               return true;
          });
     }

     // Remove validation styling on input
     document.querySelectorAll('#locationForm input, #locationForm textarea').forEach(field => {
          field.addEventListener('input', function () {
               this.classList.remove('is-invalid');
          });

          field.addEventListener('change', function () {
               this.classList.remove('is-invalid');
          });
     });
});

// Debug function to check form state
function debugFormState() {
     const form = document.getElementById('locationForm');
     const locationId = document.getElementById('locationId').value;
     const editMode = document.getElementById('editMode');

     console.log('Form Debug Info:', {
          formAction: form.action,
          formMethod: form.method,
          locationId: locationId,
          editModeExists: !!editMode,
          editModeValue: editMode ? editMode.value : 'N/A',
          isEditMode: locationId !== '0',
          editingLocationId: editingLocationId,
          submitButtonText: document.getElementById('submitLocationBtn').textContent
     });
}