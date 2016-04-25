namespace Assets
{
    public class Title
    {
        public PersonOfInterest holder = null;
        public int wealthGain = 0;
        public string name = "";

        public Title(PersonOfInterest poi)
        {
            holder = poi;
            poi.heldTitles.Add(this);
            CityContext.context._titles.Add(this);
        }

        public Title()
        {
            CityContext.context._titles.Add(this);
        }

        public void AwardTo(PersonOfInterest newHolder)
        {
            if (holder != null)
            {
                holder.heldTitles.Remove(this);
            }
            holder = newHolder;
            newHolder.heldTitles.Add(this);
        }

        public static void Tick()
        {
            foreach (Title title in CityContext.context._titles)
            {
                title.holder.wealth += title.wealthGain;
            }
        }
    }
}