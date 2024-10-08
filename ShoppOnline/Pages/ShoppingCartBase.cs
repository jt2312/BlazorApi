﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ShopOnlineModels.Dtos;
using ShoppOnline.Services.Interfaces;

namespace ShoppOnline.Pages
{
    public class ShoppingCartBase : ComponentBase
    {
        [Inject]
        public IJSRuntime Js {  get; set; }

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }
        public List<CartItemDTO> ShoppingCartItems { get; set; }

        public string ErrorMessage { get; set; }

        protected string TotalPrice { get; set; }
        protected int TotalQuantity { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
                CartChanged();
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
            }
        }

        protected async Task UpdateQty_Input(int id)
        {
            await MakeUpdateQtyButtonVisible(id , true); 
        }
        private async Task MakeUpdateQtyButtonVisible(int id, bool visible)
        {
            await Js.InvokeVoidAsync("MakeUpdateQtyButtonVisible", id, visible);
        }

        protected async Task DeleteCartItem_Click(int id)
        {
            var cartItemDto = await ShoppingCartService.DeleteItem(id);

            RemoveCartItem(id);
            CartChanged();
        }

        private void UpdateItemTotalPrice(CartItemDTO cartItemDTO)
        {
            var item = GetCartItem(cartItemDTO.Id);
            if (item != null) 
            {
                item.TotalPrice = cartItemDTO.Price * cartItemDTO.Qty;
            }
        }

        private void CalculateCartSummaryTotals()
        {
            SetTotalPrice();
            SetTotalQuantity();
        }
        private void SetTotalPrice()
        {
            TotalPrice = this.ShoppingCartItems.Sum(p => p.TotalPrice).ToString("C");
        }
        private void SetTotalQuantity()
        {
            TotalQuantity = this.ShoppingCartItems.Sum(p => p.Qty);
        }
        private CartItemDTO GetCartItem(int id)
        {
            return ShoppingCartItems.FirstOrDefault(i => i.Id == id);

        }
        private void RemoveCartItem(int id)
        {
            var CartItemDto= GetCartItem(id);
            ShoppingCartItems.Remove(CartItemDto);
        }


        protected async Task UpdateQtyCartItem_Click(int id, int qty)
        {
            try
            {
                if (qty > 0)
                {
                    var updateItemDto = new CartItemQtyUpdateDTO
                    {
                        CartItemId = id,
                        Qty = qty,
                    };
                    var returnedUpdateItemDto = await this.ShoppingCartService.UpdateQty(updateItemDto);
                    UpdateItemTotalPrice(returnedUpdateItemDto);
                    CartChanged();
                    await MakeUpdateQtyButtonVisible(id, false);

                }
                else
                {
                    var item = this.ShoppingCartItems.FirstOrDefault(i => i.Id ==id);
                    if (item !=null)
                    {
                        item.Qty = 1;
                        item.TotalPrice = item.Price;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        private void CartChanged()
        {
            CalculateCartSummaryTotals() ;
            ShoppingCartService.RaiseEventOnShoppingCartChanged(TotalQuantity);
        }
    }
}
