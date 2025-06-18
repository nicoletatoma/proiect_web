// Events public page functionality
let debounceTimer;
let allEvents = [];

document.addEventListener('DOMContentLoaded', function () {
     // Initialize filters
     initializeFilters();

     // Store initial events data
     storeEventsData();

     // Set up event listeners
     setupEventListeners();
});

function initializeFilters() {
     // Set minimum date to today
     const dateFilter = document.getElementById('dateFilter');
     if (dateFilter) {
          const today = new Date().toISOString().split('T')[0];
          dateFilter.min = today;
     }
}

function storeEventsData() {
     // Store all events data for client-side filtering
     const eventItems = document.querySelectorAll('.event-item');
     allEvents = Array.from(eventItems).map(item => {
          return {
               element: item,
               name: item.dataset.name,
               category: item.dataset.category,
               location: item.dataset.location,
               price: parseFloat(item.dataset.price),
               date: item.dataset.date,
               dateObj: new Date(item.dataset.date)
          };
     });
}

function setupEventListeners() {
     // Search input
     const searchInput = document.getElementById('searchInput');
     if (searchInput) {
          searchInput.addEventListener('input', function () {
               clearTimeout(debounceTimer);
               debounceTimer = setTimeout(applyFilters, 300);
          });
     }

     // Category filters
     document.querySelectorAll('input[name="category"]').forEach(radio => {
          radio.addEventListener('change', applyFilters);
     });

     // Location filters
     document.querySelectorAll('input[name="location"]').forEach(radio => {
          radio.addEventListener('change', applyFilters);
     });

     // Date filter
     const dateFilter = document.getElementById('dateFilter');
     if (dateFilter) {
          dateFilter.addEventListener('change', applyFilters);
     }

     // Price filters
     const priceMin = document.getElementById('price-min');
     const priceMax = document.getElementById('price-max');

     if (priceMin) {
          priceMin.addEventListener('input', function () {
               clearTimeout(debounceTimer);
               debounceTimer = setTimeout(applyFilters, 500);
          });
     }

     if (priceMax) {
          priceMax.addEventListener('input', function () {
               clearTimeout(debounceTimer);
               debounceTimer = setTimeout(applyFilters, 500);
          });
     }
}

function applyFilters() {
     // Get filter values
     const searchTerm = document.getElementById('searchInput').value.toLowerCase();
     const selectedCategory = document.querySelector('input[name="category"]:checked').value;
     const selectedLocation = document.querySelector('input[name="location"]:checked').value;
     const selectedDate = document.getElementById('dateFilter').value;
     const minPrice = parseFloat(document.getElementById('price-min').value) || 0;
     const maxPrice = parseFloat(document.getElementById('price-max').value) || Infinity;

     let filteredEvents = allEvents.filter(event => {
          // Search filter
          if (searchTerm && !event.name.includes(searchTerm)) {
               return false;
          }

          // Category filter
          if (selectedCategory !== 'toate' && event.category !== selectedCategory) {
               return false;
          }

          // Location filter
          if (selectedLocation !== 'toate' && event.location !== selectedLocation) {
               return false;
          }

          // Date filter
          if (selectedDate && event.date !== selectedDate) {
               return false;
          }

          // Price filter
          if (event.price < minPrice || event.price > maxPrice) {
               return false;
          }

          return true;
     });

     // Show/hide events
     allEvents.forEach(event => {
          event.element.style.display = 'none';
     });

     filteredEvents.forEach(event => {
          event.element.style.display = 'block';
     });

     // Update events count
     document.getElementById('eventsCount').textContent = filteredEvents.length;

     // Show no results message if needed
     showNoResultsMessage(filteredEvents.length === 0);

     // Update URL parameters
     updateURLParameters(searchTerm, selectedCategory, selectedLocation, selectedDate, minPrice, maxPrice);
}

function showNoResultsMessage(show) {
     let noResultsDiv = document.getElementById('noResultsMessage');

     if (show) {
          if (!noResultsDiv) {
               noResultsDiv = document.createElement('div');
               noResultsDiv.id = 'noResultsMessage';
               noResultsDiv.className = 'col-12';
               noResultsDiv.innerHTML = `
                <div class="alert alert-info text-center">
                    <i class="bi bi-search" style="font-size: 3rem; color: #6c757d;"></i>
                    <h4 class="mt-3">Nu s-au găsit evenimente</h4>
                    <p>Încearcă să modifici filtrele pentru a găsi evenimente.</p>
                    <button class="btn btn-outline-primary" onclick="resetFilters()">Resetează filtrele</button>
                </div>
            `;
               document.getElementById('eventsGrid').appendChild(noResultsDiv);
          }
          noResultsDiv.style.display = 'block';
     } else {
          if (noResultsDiv) {
               noResultsDiv.style.display = 'none';
          }
     }
}

function updateURLParameters(search, category, location, date, minPrice, maxPrice) {
     const params = new URLSearchParams();

     if (search) params.set('search', search);
     if (category !== 'toate') params.set('category', category);
     if (location !== 'toate') params.set('location', location);
     if (date) params.set('date', date);
     if (minPrice > 0) params.set('minPrice', minPrice);
     if (maxPrice < Infinity) params.set('maxPrice', maxPrice);

     const newURL = window.location.pathname + (params.toString() ? '?' + params.toString() : '');
     window.history.replaceState({}, '', newURL);
}

function sortEvents(sortBy) {
     let sortedEvents = [...allEvents];

     switch (sortBy) {
          case 'name-asc':
               sortedEvents.sort((a, b) => a.name.localeCompare(b.name));
               break;
          case 'name-desc':
               sortedEvents.sort((a, b) => b.name.localeCompare(a.name));
               break;
          case 'date-asc':
               sortedEvents.sort((a, b) => a.dateObj - b.dateObj);
               break;
          case 'date-desc':
               sortedEvents.sort((a, b) => b.dateObj - a.dateObj);
               break;
          case 'price-asc':
               sortedEvents.sort((a, b) => a.price - b.price);
               break;
          case 'price-desc':
               sortedEvents.sort((a, b) => b.price - a.price);
               break;
     }

     // Reorder DOM elements
     const container = document.getElementById('eventsGrid');
     const noResultsMessage = document.getElementById('noResultsMessage');

     // Remove no results message temporarily
     if (noResultsMessage) {
          noResultsMessage.remove();
     }

     sortedEvents.forEach(event => {
          container.appendChild(event.element);
     });

     // Re-add no results message if it existed
     if (noResultsMessage) {
          container.appendChild(noResultsMessage);
     }

     // Update allEvents array order
     allEvents = sortedEvents;
}

function resetFilters() {
     // Reset all form inputs
     document.getElementById('searchInput').value = '';
     document.querySelector('input[name="category"][value="toate"]').checked = true;
     document.querySelector('input[name="location"][value="toate"]').checked = true;
     document.getElementById('dateFilter').value = '';
     document.getElementById('price-min').value = '';
     document.getElementById('price-max').value = '';

     // Reset hidden categories and locations
     document.querySelectorAll('.hidden').forEach(item => {
          item.classList.add('hidden');
     });

     // Reset toggle buttons
     document.querySelectorAll('.toggle-btn').forEach(btn => {
          btn.textContent = 'Afișează mai mult';
     });

     // Apply filters (will show all events)
     applyFilters();
}

// Toggle function for filter lists (from services.js)
function toggleOptions(button) {
     const listItems = button.previousElementSibling.getElementsByTagName("li");
     const listItemsArray = Array.from(listItems);

     // Toggle for hidden elements
     listItemsArray.slice(5).forEach(item => item.classList.toggle("hidden"));

     // Check if there are hidden elements
     const hiddenItems = listItemsArray.filter(item => item.classList.contains("hidden"));

     // Change button text based on elements state
     if (hiddenItems.length > 0) {
          button.textContent = "Afișează mai mult";
     } else {
          button.textContent = "Ascunde";
     }
}

// Toggle filters panel on mobile
document.addEventListener('DOMContentLoaded', function () {
     const toggleBtn = document.getElementById('toggleFiltersBtn');
     const filtersPanel = document.getElementById('filtersPanel');

     if (toggleBtn && filtersPanel) {
          toggleBtn.addEventListener('click', function () {
               filtersPanel.classList.toggle('show-filters');
               if (filtersPanel.classList.contains('show-filters')) {
                    toggleBtn.innerHTML = '<i class="fas fa-times"></i> Ascunde filtre';
               } else {
                    toggleBtn.innerHTML = '<i class="fas fa-filter"></i> Afișează filtre';
               }
          });
     }

     // Hide filters panel on small screens by default
     function checkWidth() {
          if (window.innerWidth < 768) {
               if (filtersPanel) {
                    filtersPanel.classList.remove('show-filters');
               }
               if (toggleBtn) {
                    toggleBtn.innerHTML = '<i class="fas fa-filter"></i> Afișează filtre';
               }
          } else {
               if (filtersPanel) {
                    filtersPanel.classList.add('show-filters');
               }
          }
     }

     // Initial check
     checkWidth();

     // Check on resize
     window.addEventListener('resize', checkWidth);
});

// Initialize filters from URL parameters on page load
document.addEventListener('DOMContentLoaded', function () {
     const urlParams = new URLSearchParams(window.location.search);

     // Set search term
     const search = urlParams.get('search');
     if (search) {
          document.getElementById('searchInput').value = search;
     }

     // Set category
     const category = urlParams.get('category');
     if (category) {
          const categoryRadio = document.querySelector(`input[name="category"][value="${category}"]`);
          if (categoryRadio) {
               categoryRadio.checked = true;
          }
     }

     // Set location
     const location = urlParams.get('location');
     if (location) {
          const locationRadio = document.querySelector(`input[name="location"][value="${location}"]`);
          if (locationRadio) {
               locationRadio.checked = true;
          }
     }

     // Set date
     const date = urlParams.get('date');
     if (date) {
          document.getElementById('dateFilter').value = date;
     }

     // Set price range
     const minPrice = urlParams.get('minPrice');
     if (minPrice) {
          document.getElementById('price-min').value = minPrice;
     }

     const maxPrice = urlParams.get('maxPrice');
     if (maxPrice) {
          document.getElementById('price-max').value = maxPrice;
     }
});

// Add smooth scrolling for category/location links
document.addEventListener('DOMContentLoaded', function () {
     // Smooth scroll to top when filters change
     const allFilterInputs = document.querySelectorAll('input[name="category"], input[name="location"], #searchInput, #dateFilter, #price-min, #price-max');

     allFilterInputs.forEach(input => {
          input.addEventListener('change', function () {
               // Small delay to let the filtering complete
               setTimeout(() => {
                    document.querySelector('.results-container').scrollIntoView({
                         behavior: 'smooth',
                         block: 'start'
                    });
               }, 100);
          });
     });
});