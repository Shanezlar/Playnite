using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Playnite.Providers.Humble
{
    
    public class GetOrdersResult
    {
        public string gamekey { get; set; }
    }

    public class GetOrderInfoResult
    {
        public double amount_spent { get; set; }
        public Product product { get; set; }
        public string gamekey { get; set; }
        public string uid { get; set; }
        public DateTime created { get; set; }
        public object missed_credit { get; set; }
        public List<Subproduct> subproducts { get; set; }
        public TpkdDict tpkd_dict { get; set; }
        public string currency { get; set; }
        public bool is_giftee { get; set; }
        public bool claimed { get; set; }
        public double total { get; set; }
        public List<string> path_ids { get; set; }
    }


    public class AutomatedEmptyTpkds
    {
    }

    public class Product
    {
        public string category { get; set; }
        public string machine_name { get; set; }
        public string post_purchase_text { get; set; }
        public bool supports_canonical { get; set; }
        public string human_name { get; set; }
        public AutomatedEmptyTpkds automated_empty_tpkds { get; set; }
        public bool partial_gift_enabled { get; set; }
    }

    public class Url
    {
        public string web { get; set; }
        public string bittorrent { get; set; }
    }

    public class DownloadStruct
    {
        public string sha1 { get; set; }
        public string name { get; set; }
        public Url url { get; set; }
        public int timestamp { get; set; }
        public DateTime uploaded_at { get; set; }
        public long file_size { get; set; }
        public int small { get; set; }
        public string md5 { get; set; }
        public string human_size { get; set; }
    }

    public class OptionsDict
    {
    }

    public class Download
    {
        public string machine_name { get; set; }
        public string platform { get; set; }
        public List<DownloadStruct> download_struct { get; set; }
        public OptionsDict options_dict { get; set; }
        public string download_identifier { get; set; }
        public bool android_app_only { get; set; }
        public object download_version_number { get; set; }
    }

    public class Payee
    {
        public string human_name { get; set; }
        public string machine_name { get; set; }
    }

    public class Subproduct
    {
        public string machine_name { get; set; }
        public string url { get; set; }
        public List<Download> downloads { get; set; }
        public object library_family_name { get; set; }
        public Payee payee { get; set; }
        public string human_name { get; set; }
        public string custom_download_page_box_html { get; set; }
        public string icon { get; set; }
    }

    public class AllTpk
    {
        public List<object> exclusive_platforms { get; set; }
        public string machine_name { get; set; }
        public bool is_current_version { get; set; }
        public int keyindex { get; set; }
        public List<object> tpkds_to_supersede { get; set; }
        public string key_type { get; set; }
        public bool visible { get; set; }
        public bool display_separately { get; set; }
        public string redeemed_key_val { get; set; }
        public bool auto_redeem { get; set; }
        public object steam_app_id { get; set; }
        public List<object> disallowed_platforms { get; set; }
        public bool show_custom_instructions_in_user_libraries { get; set; }
        public List<object> exclusive_countries { get; set; }
        public string @class { get; set; }
        public bool show_tpkd_warning_message_on_product { get; set; }
        public bool permanently_depletable { get; set; }
        public string payee { get; set; }
        public bool is_gift { get; set; }
        public string gamekey { get; set; }
        public string version_changelog { get; set; }
        public DateTime created { get; set; }
        public bool notify_ops_when_low { get; set; }
        public List<object> disallowed_countries { get; set; }
        public DateTime made_current_at { get; set; }
        public List<object> contained_subproducts { get; set; }
        public string instructions_html { get; set; }
        public bool send_deactivated_keys_to_developers { get; set; }
        public string key_type_human_name { get; set; }
        public List<object> platforms { get; set; }
        public string human_name { get; set; }
        public string preinstruction_text { get; set; }
        public bool auto_expand { get; set; }
        public string disclaimer { get; set; }
    }

    public class TpkdDict
    {
        public List<AllTpk> all_tpks { get; set; }
    }



}
