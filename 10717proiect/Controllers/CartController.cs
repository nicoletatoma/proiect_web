using _10717proiect.BusinessLogic.Interfaces;
using _10717proiect.Models.Cart;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _10717proiect.Controllers
{

     public class CartController : BaseController
     {
          private readonly IEventManager _eventManager;
          private const string CART_SESSION_KEY = "UserCart";

          public CartController()
          {
               var bl = new BusinessLogic.BusinessLogic();
               _eventManager = bl.GetEventManagerBL();
          }

          // GET: Cart
          public ActionResult Index()
          {
               var cartItems = GetCartItems();
               return View("~/Views/Home/Cos.cshtml", cartItems);
          }

          // POST: Cart/AddToCart
          [HttpPost]
          public JsonResult AddToCart(int eventId, int quantity = 1)
          {
               try
               {
                    // Obține datele evenimentului
                    var events = _eventManager.GetPublishedEvents();
                    var eventData = events.FirstOrDefault(e => e.Id == eventId);

                    if (eventData == null)
                    {
                         return Json(new { success = false, message = "Evenimentul nu a fost găsit." });
                    }

                    // Obține coșul curent
                    var cartItems = GetCartItems();

                    // Verifică dacă evenimentul este deja în coș
                    var existingItem = cartItems.FirstOrDefault(c => c.EventId == eventId);

                    if (existingItem != null)
                    {
                         // Actualizează cantitatea
                         existingItem.Quantity += quantity;
                    }
                    else
                    {
                         // Adaugă un element nou în coș
                         var cartItem = new CartItem
                         {
                              EventId = eventData.Id,
                              EventName = eventData.eventName,
                              EventImage = eventData.eventImage,
                              EventDate = eventData.eventDate,
                              EventLocation = eventData.eventLocation,
                              EventCategory = eventData.eventCategory,
                              Price = eventData.eventPrice,
                              Quantity = quantity,
                              AddedAt = DateTime.Now
                         };

                         cartItems.Add(cartItem);
                    }

                    // Salvează coșul în sesiune
                    SaveCartItems(cartItems);

                    return Json(new
                    {
                         success = true,
                         message = $"Bilet adăugat în coș cu succes!",
                         cartCount = cartItems.Sum(c => c.Quantity),
                         eventName = eventData.eventName
                    });
               }
               catch (Exception ex)
               {
                    System.Diagnostics.Debug.WriteLine($"Error in AddToCart: {ex.Message}");
                    return Json(new { success = false, message = "A apărut o eroare la adăugarea în coș." });
               }
          }

          // POST: Cart/UpdateQuantity
          [HttpPost]
          public JsonResult UpdateQuantity(int eventId, int quantity)
          {
               try
               {
                    var cartItems = GetCartItems();
                    var item = cartItems.FirstOrDefault(c => c.EventId == eventId);

                    if (item != null)
                    {
                         if (quantity <= 0)
                         {
                              cartItems.Remove(item);
                         }
                         else
                         {
                              item.Quantity = quantity;
                         }

                         SaveCartItems(cartItems);

                         var totalPrice = cartItems.Sum(c => c.TotalPrice);

                         return Json(new
                         {
                              success = true,
                              itemTotal = item?.TotalPrice ?? 0,
                              cartTotal = totalPrice,
                              cartCount = cartItems.Sum(c => c.Quantity)
                         });
                    }

                    return Json(new { success = false, message = "Elementul nu a fost găsit în coș." });
               }
               catch (Exception ex)
               {
                    System.Diagnostics.Debug.WriteLine($"Error in UpdateQuantity: {ex.Message}");
                    return Json(new { success = false, message = "A apărut o eroare la actualizarea cantității." });
               }
          }

          // POST: Cart/RemoveFromCart
          [HttpPost]
          public JsonResult RemoveFromCart(int eventId)
          {
               try
               {
                    var cartItems = GetCartItems();
                    var item = cartItems.FirstOrDefault(c => c.EventId == eventId);

                    if (item != null)
                    {
                         cartItems.Remove(item);
                         SaveCartItems(cartItems);

                         return Json(new
                         {
                              success = true,
                              message = "Elementul a fost eliminat din coș.",
                              cartCount = cartItems.Sum(c => c.Quantity)
                         });
                    }

                    return Json(new { success = false, message = "Elementul nu a fost găsit în coș." });
               }
               catch (Exception ex)
               {
                    System.Diagnostics.Debug.WriteLine($"Error in RemoveFromCart: {ex.Message}");
                    return Json(new { success = false, message = "A apărut o eroare la eliminarea din coș." });
               }
          }

          // POST: Cart/ClearCart
          [HttpPost]
          public JsonResult ClearCart()
          {
               try
               {
                    Session[CART_SESSION_KEY] = null;
                    return Json(new { success = true, message = "Coșul a fost golit." });
               }
               catch (Exception ex)
               {
                    System.Diagnostics.Debug.WriteLine($"Error in ClearCart: {ex.Message}");
                    return Json(new { success = false, message = "A apărut o eroare la golirea coșului." });
               }
          }

          // GET: Cart/GetCartCount
          public JsonResult GetCartCount()
          {
               var cartItems = GetCartItems();
               return Json(new { count = cartItems.Sum(c => c.Quantity) }, JsonRequestBehavior.AllowGet);
          }

          // Metode helper private
          private List<CartItem> GetCartItems()
          {
               var cartJson = Session[CART_SESSION_KEY] as string;
               if (string.IsNullOrEmpty(cartJson))
               {
                    return new List<CartItem>();
               }

               try
               {
                    return JsonConvert.DeserializeObject<List<CartItem>>(cartJson) ?? new List<CartItem>();
               }
               catch
               {
                    return new List<CartItem>();
               }
          }

          private void SaveCartItems(List<CartItem> cartItems)
          {
               var cartJson = JsonConvert.SerializeObject(cartItems);
               Session[CART_SESSION_KEY] = cartJson;
          }
     }
}