let editingEventId = null;

function showEventForm() {
     document.getElementById('eventForm').classList.remove('d-none');
     document.getElementById('eventName').focus();

     // Only reset form if it's not an edit operation
     if (!editingEventId) {
          // Reset form for new event
          document.getElementById('eventId').value = '0';
          document.getElementById('eventForm').action = '/Admin/AddEvent';
          document.getElementById('submitEventBtn').textContent = 'Publică Evenimentul';

          // Remove edit mode indicator if it exists
          const editModeInput = document.getElementById('editMode');
          if (editModeInput) {
               editModeInput.remove();
          }
     }
}

function hideEventForm() {
     document.getElementById('eventForm').classList.add('d-none');
     clearEventForm();
     editingEventId = null;

     // Reset form to add mode
     document.getElementById('eventForm').action = '/Admin/AddEvent';
     document.getElementById('submitEventBtn').textContent = 'Publică Evenimentul';

     // Remove edit mode indicator
     const editModeInput = document.getElementById('editMode');
     if (editModeInput) {
          editModeInput.remove();
     }
}

function clearEventForm() {
     document.getElementById('eventId').value = '0';
     document.getElementById('eventName').value = '';
     document.getElementById('eventDescription').value = '';
     document.getElementById('eventDate').value = '';
     document.getElementById('eventLocation').value = '';
     document.getElementById('eventCategory').value = '';
     document.getElementById('eventPrice').value = '';
     document.getElementById('eventStatus').value = '0';
     document.getElementById('eventImageFile').value = '';
     document.getElementById('imagePreview').classList.add('d-none');

     // Remove validation classes
     document.querySelectorAll('#eventForm .is-invalid').forEach(field => {
          field.classList.remove('is-invalid');
     });

     // Remove edit mode indicator
     const editModeInput = document.getElementById('editMode');
     if (editModeInput) {
          editModeInput.remove();
     }
}

function previewEventImage(input) {
     if (input.files && input.files[0]) {
          const reader = new FileReader();

          reader.onload = function (e) {
               document.getElementById('previewImg').src = e.target.result;
               document.getElementById('imagePreview').classList.remove('d-none');
          };

          reader.readAsDataURL(input.files[0]);
     }
}

function saveDraft() {
     // Set status to draft
     document.getElementById('eventStatus').value = '0';

     // Update button text temporarily
     const submitBtn = document.getElementById('submitEventBtn');
     const originalText = submitBtn.textContent;
     submitBtn.textContent = 'Salvează Draft';

     // Submit the form
     const form = document.getElementById('eventForm');
     form.submit();

     // Note: The page will reload after submit, so we don't need to restore the button text
}

function editEvent(eventId) {
     editingEventId = eventId;

     // Get event data via AJAX
     fetch(`/Admin/GetEvent?eventId=${eventId}`, {
          method: 'GET',
          headers: {
               'Content-Type': 'application/json'
          }
     })
          .then(response => response.json())
          .then(data => {
               if (data.success) {
                    // Clear form first
                    clearEventForm();

                    // Populate form with event data
                    document.getElementById('eventId').value = data.data.id;
                    document.getElementById('eventName').value = data.data.eventName;
                    document.getElementById('eventDescription').value = data.data.eventDescription || '';
                    document.getElementById('eventDate').value = data.data.eventDate;
                    document.getElementById('eventLocation').value = data.data.eventLocation;
                    document.getElementById('eventCategory').value = data.data.eventCategory;
                    document.getElementById('eventPrice').value = data.data.eventPrice;
                    document.getElementById('eventStatus').value = data.data.eventStatus;

                    // Show existing image if available
                    if (data.data.eventImage) {
                         document.getElementById('previewImg').src = data.data.eventImage;
                         document.getElementById('imagePreview').classList.remove('d-none');
                    }

                    // Update form for editing
                    const form = document.getElementById('eventForm');
                    form.action = '/Admin/UpdateEvent';
                    form.method = 'POST';

                    // Update submit button
                    const submitBtn = document.getElementById('submitEventBtn');
                    submitBtn.textContent = 'Actualizează Evenimentul';
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
                    document.getElementById('eventForm').classList.remove('d-none');
                    document.getElementById('eventName').focus();

               } else {
                    showNotification('error', data.message || 'Nu s-a putut încărca evenimentul.');
               }
          })
          .catch(error => {
               console.error('Error:', error);
               showNotification('error', 'A apărut o eroare la încărcarea evenimentului.');
          });
}

function deleteEvent(eventId) {
     if (confirm('Sigur doriți să ștergeți acest eveniment? Această acțiune este ireversibilă!')) {
          const formData = new FormData();
          formData.append('eventId', eventId);

          // Add CSRF token
          const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
          formData.append('__RequestVerificationToken', token);

          fetch('/Admin/DeleteEvent', {
               method: 'POST',
               body: formData
          })
               .then(response => response.json())
               .then(data => {
                    if (data.success) {
                         // Remove event card from DOM
                         const eventCard = document.querySelector(`[data-event-id="${eventId}"]`);
                         if (eventCard) {
                              eventCard.remove();
                         }

                         // Check if there are no more events
                         const eventList = document.getElementById('eventList');
                         if (eventList.children.length === 0) {
                              eventList.innerHTML = `
                        <div id="emptyState" class="col-12 text-center py-5">
                            <i class="bi bi-calendar-event" style="font-size: 3rem; color: #6c757d;"></i>
                            <p class="mt-3 text-muted">Nu există evenimente adăugate momentan.</p>
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
                    showNotification('error', 'A apărut o eroare la ștergerea evenimentului.');
               });
     }
}

function toggleEventStatus(eventId, newStatus) {
     const formData = new FormData();
     formData.append('eventId', eventId);
     formData.append('status', newStatus);

     // Add CSRF token
     const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
     formData.append('__RequestVerificationToken', token);

     fetch('/Admin/UpdateEventStatus', {
          method: 'POST',
          body: formData
     })
          .then(response => response.json())
          .then(data => {
               if (data.success) {
                    // Update status badge and buttons
                    updateEventStatusUI(eventId, newStatus);
                    showNotification('success', data.message);
               } else {
                    showNotification('error', data.message);
               }
          })
          .catch(error => {
               console.error('Error:', error);
               showNotification('error', 'A apărut o eroare la actualizarea statusului.');
          });
}

function publishEvent(eventId) {
     toggleEventStatus(eventId, 1); // 1 = Published
}

function updateEventStatusUI(eventId, newStatus) {
     const eventCard = document.querySelector(`[data-event-id="${eventId}"]`);
     if (!eventCard) return;

     const statusBadge = eventCard.querySelector('.event-status .badge');
     const actionsContainer = eventCard.querySelector('.event-actions');

     // Update status badge
     switch (parseInt(newStatus)) {
          case 0: // Draft
               statusBadge.className = 'badge bg-warning';
               statusBadge.textContent = 'Draft';
               break;
          case 1: // Published
               statusBadge.className = 'badge bg-success';
               statusBadge.textContent = 'Publicat';
               break;
          case 2: // Suspended
               statusBadge.className = 'badge bg-secondary';
               statusBadge.textContent = 'Suspendat';
               break;
          case 3: // Cancelled
               statusBadge.className = 'badge bg-danger';
               statusBadge.textContent = 'Anulat';
               break;
          case 4: // Finished
               statusBadge.className = 'badge bg-dark';
               statusBadge.textContent = 'Finalizat';
               break;
     }

     // Update action buttons
     const editBtn = `<button class="btn btn-sm btn-outline-primary me-1" onclick="editEvent(${eventId})">
                        <i class="bi bi-pencil"></i>
                     </button>`;

     const deleteBtn = `<button class="btn btn-sm btn-outline-danger" onclick="deleteEvent(${eventId})">
                          <i class="bi bi-trash"></i>
                       </button>`;

     let statusBtn = '';

     switch (parseInt(newStatus)) {
          case 0: // Draft
               statusBtn = `<button class="btn btn-sm btn-outline-success me-1" onclick="publishEvent(${eventId})">
                            <i class="bi bi-play"></i>
                         </button>`;
               break;
          case 1: // Published
               statusBtn = `<button class="btn btn-sm btn-outline-warning me-1" onclick="toggleEventStatus(${eventId}, 2)">
                            <i class="bi bi-pause"></i>
                         </button>`;
               break;
          case 2: // Suspended
               statusBtn = `<button class="btn btn-sm btn-outline-success me-1" onclick="toggleEventStatus(${eventId}, 1)">
                            <i class="bi bi-play"></i>
                         </button>`;
               break;
     }

     actionsContainer.innerHTML = editBtn + statusBtn + deleteBtn;
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
     const eventForm = document.getElementById('eventForm');

     if (eventForm) {
          eventForm.addEventListener('submit', function (e) {
               // Basic client-side validation
               const requiredFields = ['eventName', 'eventDate', 'eventLocation', 'eventCategory', 'eventPrice'];
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

               // Validate event date is in the future (only for new events, allow past dates for editing)
               const eventId = document.getElementById('eventId').value;
               const eventDate = new Date(document.getElementById('eventDate').value);
               const now = new Date();

               if (eventId === '0' && eventDate <= now) {
                    document.getElementById('eventDate').classList.add('is-invalid');
                    showNotification('error', 'Data evenimentului trebuie să fie în viitor.');
                    isValid = false;
               }

               // Validate price is positive
               const price = parseFloat(document.getElementById('eventPrice').value);
               if (price < 0) {
                    document.getElementById('eventPrice').classList.add('is-invalid');
                    showNotification('error', 'Prețul trebuie să fie mai mare sau egal cu 0.');
                    isValid = false;
               }

               if (!isValid) {
                    e.preventDefault();
                    showNotification('error', 'Vă rugăm să completați corect toate câmpurile obligatorii.');
                    return false;
               }

               // Log form submission for debugging
               const formAction = eventForm.action;
               const eventIdValue = document.getElementById('eventId').value;
               console.log('Form submission:', {
                    action: formAction,
                    eventId: eventIdValue,
                    isEdit: eventIdValue !== '0'
               });

               return true;
          });
     }

     // Remove validation styling on input
     document.querySelectorAll('#eventForm input, #eventForm select, #eventForm textarea').forEach(field => {
          field.addEventListener('input', function () {
               this.classList.remove('is-invalid');
          });

          field.addEventListener('change', function () {
               this.classList.remove('is-invalid');
          });
     });

     // Set minimum date for event date input to today (only for new events)
     const eventDateInput = document.getElementById('eventDate');
     if (eventDateInput) {
          const now = new Date();
          const year = now.getFullYear();
          const month = String(now.getMonth() + 1).padStart(2, '0');
          const day = String(now.getDate()).padStart(2, '0');
          const hours = String(now.getHours()).padStart(2, '0');
          const minutes = String(now.getMinutes()).padStart(2, '0');

          const minDateTime = `${year}-${month}-${day}T${hours}:${minutes}`;

          // Only set min date for new events, allow editing past events
          eventDateInput.addEventListener('focus', function () {
               const eventId = document.getElementById('eventId').value;
               if (eventId === '0') {
                    this.min = minDateTime;
               } else {
                    this.removeAttribute('min');
               }
          });
     }
});

// Debug function to check form state
function debugFormState() {
     const form = document.getElementById('eventForm');
     const eventId = document.getElementById('eventId').value;
     const editMode = document.getElementById('editMode');

     console.log('Form Debug Info:', {
          formAction: form.action,
          formMethod: form.method,
          eventId: eventId,
          editModeExists: !!editMode,
          editModeValue: editMode ? editMode.value : 'N/A',
          isEditMode: eventId !== '0',
          editingEventId: editingEventId,
          submitButtonText: document.getElementById('submitEventBtn').textContent
     });
}

// Add debug button (temporary - remove in production)
document.addEventListener('DOMContentLoaded', function () {
     // Add debug button for testing
     const debugBtn = document.createElement('button');
     debugBtn.textContent = 'Debug Form';
     debugBtn.type = 'button';
     debugBtn.className = 'btn btn-info btn-sm';
     debugBtn.onclick = debugFormState;
     debugBtn.style.position = 'fixed';
     debugBtn.style.top = '10px';
     debugBtn.style.right = '10px';
     debugBtn.style.zIndex = '9999';
     document.body.appendChild(debugBtn);
});