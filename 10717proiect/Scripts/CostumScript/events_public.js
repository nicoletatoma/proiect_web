// Events public page functionality
let debounceTimer;
let allEvents = [];

document.addEventListener('DOMContentLoaded', function () {
     // Delay initialization to ensure DOM is fully loaded
     setTimeout(() => {
          console.log('Initializing filters...');

          // Check if all required elements exist
          const requiredElements = {
               searchInput: document.getElementById('searchInput'),
               categoryRadios: document.querySelectorAll('input[name="category"]'),
               dateFilter: document.getElementById('dateFilter'),
               priceMin: document.getElementById('price-min'),
               priceMax: document.getElementById('price-max'),
               eventsGrid: document.getElementById('eventsGrid')
          };

          console.log('Required elements check:', requiredElements);

          // Only proceed if all elements exist
          if (requiredElements.searchInput && requiredElements.categoryRadios.length > 0 &&
               requiredElements.dateFilter && requiredElements.priceMin &&
               requiredElements.priceMax && requiredElements.eventsGrid) {

               // Initialize filters
               initializeFilters();

               // Store initial events data
               storeEventsData();

               // Set up event listeners
               setupEventListeners();

               console.log('Filters initialized successfully');
          } else {
               console.error('Missing required elements for filters');
          }
     }, 100);
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
     console.log('applyFilters called');

     // Get filter values with null checks
     const searchInput = document.getElementById('searchInput');
     const categoryChecked = document.querySelector('input[name="category"]:checked');
     const dateFilter = document.getElementById('dateFilter');
     const priceMinInput = document.getElementById('price-min');
     const priceMaxInput = document.getElementById('price-max');
     const eventsCountElement = document.getElementById('eventsCount');

     // Debug log
     console.log('Elements found:', {
          searchInput: !!searchInput,
          categoryChecked: !!categoryChecked,
          dateFilter: !!dateFilter,
          priceMinInput: !!priceMinInput,
          priceMaxInput: !!priceMaxInput,
          eventsCountElement: !!eventsCountElement
     });

     if (!searchInput || !categoryChecked || !dateFilter || !priceMinInput || !priceMaxInput) {
          console.error('Some filter elements not found - aborting filter');
          return;
     }

     const searchTerm = searchInput.value.toLowerCase();
     const selectedCategory = categoryChecked.value;
     const selectedDate = dateFilter.value;
     const minPrice = parseFloat(priceMinInput.value) || 0;
     const maxPrice = parseFloat(priceMaxInput.value) || Infinity;

     let filteredEvents = allEvents.filter(event => {
          // Search filter
          if (searchTerm && !event.name.includes(searchTerm)) {
               return false;
          }

          // Category filter
          if (selectedCategory !== 'toate' && event.category !== selectedCategory) {
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

     console.log('Filters applied:', {
          searchTerm,
          selectedCategory,
          selectedDate,
          minPrice,
          maxPrice,
          totalEvents: allEvents.length,
          filteredEvents: filteredEvents.length
     });

     // Show/hide events
     allEvents.forEach(event => {
          event.element.style.display = 'none';
     });

     filteredEvents.forEach(event => {
          event.element.style.display = 'block';
     });

     // Update events count
     if (eventsCountElement) {
          eventsCountElement.textContent = filteredEvents.length;
     }

     // Show no results message if needed
     showNoResultsMessage(filteredEvents.length === 0);

     // Update URL parameters
     updateURLParameters(searchTerm, selectedCategory, selectedDate, minPrice, maxPrice);
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

function updateURLParameters(search, category, date, minPrice, maxPrice) {
     const params = new URLSearchParams();

     if (search) params.set('search', search);
     if (category !== 'toate') params.set('category', category);
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
     // Reset all form inputs with null checks
     const searchInput = document.getElementById('searchInput');
     const categoryRadio = document.querySelector('input[name="category"][value="toate"]');
     const dateFilter = document.getElementById('dateFilter');
     const priceMin = document.getElementById('price-min');
     const priceMax = document.getElementById('price-max');

     if (searchInput) searchInput.value = '';
     if (categoryRadio) categoryRadio.checked = true;
     if (dateFilter) dateFilter.value = '';
     if (priceMin) priceMin.value = '';
     if (priceMax) priceMax.value = '';

     // Reset hidden categories
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
     setTimeout(() => {
          const urlParams = new URLSearchParams(window.location.search);

          // Set search term
          const search = urlParams.get('search');
          if (search) {
               const searchInput = document.getElementById('searchInput');
               if (searchInput) searchInput.value = search;
          }

          // Set category
          const category = urlParams.get('category');
          if (category) {
               const categoryRadio = document.querySelector(`input[name="category"][value="${category}"]`);
               if (categoryRadio) {
                    categoryRadio.checked = true;
               }
          }

          // Set date
          const date = urlParams.get('date');
          if (date) {
               const dateFilter = document.getElementById('dateFilter');
               if (dateFilter) dateFilter.value = date;
          }

          // Set price range
          const minPrice = urlParams.get('minPrice');
          if (minPrice) {
               const priceMinInput = document.getElementById('price-min');
               if (priceMinInput) priceMinInput.value = minPrice;
          }

          const maxPrice = urlParams.get('maxPrice');
          if (maxPrice) {
               const priceMaxInput = document.getElementById('price-max');
               if (priceMaxInput) priceMaxInput.value = maxPrice;
          }
     }, 200);
});

// Add smooth scrolling for category links
document.addEventListener('DOMContentLoaded', function () {
     // Smooth scroll to top when filters change
     const allFilterInputs = document.querySelectorAll('input[name="category"], #searchInput, #dateFilter, #price-min, #price-max');

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