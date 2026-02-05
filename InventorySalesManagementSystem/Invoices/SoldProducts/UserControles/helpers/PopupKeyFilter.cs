namespace InventorySalesManagementSystem.Invoices.SoldProducts.UserControles.helpers
{
    public class PopupKeyFilter : IMessageFilter
    {
        private readonly ListBox _popup;
        private readonly Action _commitAction;
        private readonly Action _closeAction;

        public PopupKeyFilter(
            ListBox popup,
            Action commitAction,
            Action closeAction)
        {
            _popup = popup;
            _commitAction = commitAction;
            _closeAction = closeAction;
        }

        public bool PreFilterMessage(ref Message m)
        {
            const int WM_KEYDOWN = 0x0100;
            const int WM_CHAR = 0x0102;

            if (!_popup.Visible)
                return false;

            Keys key = Keys.None;

            if (m.Msg == WM_KEYDOWN)
                key = (Keys)m.WParam;

            else if (m.Msg == WM_CHAR && (char)m.WParam == '\r')
                key = Keys.Enter;

            if (key == Keys.Down)
            {
                _popup.SelectedIndex =
                    Math.Min(_popup.Items.Count - 1, _popup.SelectedIndex + 1);
                return true;
            }

            if (key == Keys.Up)
            {
                _popup.SelectedIndex =
                    Math.Max(0, _popup.SelectedIndex - 1);
                return true;
            }

            if (key == Keys.Enter)
            {
                _commitAction();
                return true;
            }

            if (key == Keys.Escape)
            {
                _closeAction();
                return true;
            }

            return false;
        }
    }

}
