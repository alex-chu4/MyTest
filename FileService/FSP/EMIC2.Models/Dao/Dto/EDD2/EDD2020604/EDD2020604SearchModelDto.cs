namespace EMIC2.Models.Dao.Dto.EDD2.EDD2020604
{
    public class EDD2020604SearchModelDto
    {
        public EDD2020604SearchModelDto()
        {
            this.city_name = string.Empty;
            this.town_name = string.Empty;
            this.location_id = -1;
            this.item_group_id = -1;
            this.master_type_id = -1;
            this.secondary_type_id = -1;
            this.detail_type_id = -1;
        }

        public string city_name { get; set; }

        public string town_name { get; set; }

        public int location_id { get; set; }

        public int item_group_id { get; set; }

        public int master_type_id { get; set; }

        public int secondary_type_id { get; set; }

        public int detail_type_id { get; set; }

        public string format { get; set; }
    }
}
