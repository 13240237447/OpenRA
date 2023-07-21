namespace OpenRA
{
    public class ActivityUtils
    {
        public static Activity RunActivity(Actor self, Activity act)
        {
            if (act == null)
                return act;
            
            while (act != null)
            {
                var prev = act;
                act = act.TickOuter(self);
                if (act == prev)
                    break;
            }

            return act;
        }
    }
}