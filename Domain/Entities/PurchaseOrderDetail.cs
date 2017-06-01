using Newtonsoft.Json;

namespace Carvajal.Turns.Domain.Entities
{
    public class PurchaseOrderDetail
    {
        private string _purchaseOrderSendReference;
        private string _idProduct;
        private float _quantity;
        private float _price;

        [JsonProperty(PropertyName = "purchase_order_send_reference")]
        public string PurchaseOrderSendReference
        {
            get { return _purchaseOrderSendReference; }
            set { _purchaseOrderSendReference = value; }
        }

        [JsonProperty(PropertyName = "id_product")]
        public string IdProduct
        {
            get { return _idProduct; }
            set { _idProduct = value; }
        }

        [JsonProperty(PropertyName = "quantity")]
        public float Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        [JsonProperty(PropertyName = "price")]
        public float Price
        {
            get { return _price; }
            set { _price = value; }
        }
    }
}
