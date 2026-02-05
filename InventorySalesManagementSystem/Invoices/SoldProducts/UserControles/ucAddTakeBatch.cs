using DataAccessLayer.Entities.Invoices;
using LogicLayer.DTOs.InvoiceDTO.SoldProducts;
using LogicLayer.DTOs.InvoiceDTO.TakeBatches;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySalesManagementSystem.Invoices.SoldProducts.UserControles
{
    public partial class ucAddTakeBatch : UserControl
    {
        public IServiceProvider _serviceProvider { get; private set; }


        private TakeBatchType _takeBatchType = TakeBatchType.Invoice;
        [Browsable(true)]
        [DefaultValue(TakeBatchType.Invoice)]
        public TakeBatchType takeBatchType
        {
            get { return _takeBatchType; }
            set { _takeBatchType = value;
                if (_takeBatchType == TakeBatchType.Invoice)
                {
                    lbTakeNameLable.Text = "المستلم : ";
                    ucProductSelector1.RefundMode(false);
                }
                else
                {
                    lbTakeNameLable.Text = "المُرجع : ";
                    //ucProductSelector1.GetUcListView.Visible = false;
                    //ucProductSelector1.GetUcListView.Enabled = false;
                    ucProductSelector1.RefundMode(true);
                }

            }
        }


        private bool _allowBathcData = true;
        [Browsable(true)]
        [DefaultValue(true)]
        public bool AllowBathcData
        {
            set
            {
                _allowBathcData = value;
                
                
                txtNote.Enabled = value;
                txtNote.Text = value? txtNote.Text : string.Empty;
                
                txtReciver.Enabled = value;
                txtReciver.Text = value? txtReciver.Text: string.Empty;
            }

            get
            {
                return _allowBathcData;
            }
        }



        public ucAddTakeBatch()
        {
            InitializeComponent();
        }

        public TakeBatchAddDto GetTakeBatch()
        {
            return new TakeBatchAddDto
            {
                TakeName = txtReciver.Text.Trim(),
                Notes = txtNote.Text.Trim(),
                InvoiceId = -1,//to be set later
                SoldProductAddDtos = ucProductSelector1.GetSoldProducts().ToList(),
                TakeBatchType = _takeBatchType
            };
        }

        public void Initialize(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            ucProductSelector1.Initialize(_serviceProvider);
            this.Enabled = true;
        }

        public void Initialize(IServiceProvider serviceProvider,List<SoldProductSaleDetailsListDto>products)
        {
            _serviceProvider = serviceProvider;
            ucProductSelector1.Initialize(_serviceProvider,products);
            this.Enabled = true;
        }
    }
}
