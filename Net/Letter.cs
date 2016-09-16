using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchIRC.Net
{
    internal class Letter
    {

        [JsonProperty("mail_id")]
        public object MailId { get; set; }

        [JsonProperty("mail_from")]
        public string MailFrom { get; set; }

        [JsonProperty("mail_subject")]
        public string MailSubject { get; set; }

        [JsonProperty("mail_excerpt")]
        public string MailExcerpt { get; set; }

        [JsonProperty("mail_timestamp")]
        public object MailTimestamp { get; set; }

        [JsonProperty("mail_read")]
        public object MailRead { get; set; }

        [JsonProperty("mail_date")]
        public string MailDate { get; set; }

        [JsonProperty("att")]
        public object Att { get; set; }

        [JsonProperty("mail_size")]
        public string MailSize { get; set; }

        [JsonProperty("reply_to")]
        public string ReplyTo { get; set; }

        [JsonProperty("content_type")]
        public string ContentType { get; set; }

        [JsonProperty("mail_recipient")]
        public string MailRecipient { get; set; }

        [JsonProperty("source_id")]
        public int? SourceId { get; set; }

        [JsonProperty("source_mail_id")]
        public int? SourceMailId { get; set; }

        [JsonProperty("mail_body")]
        public string MailBody { get; set; }

        [JsonProperty("size")]
        public int? Size { get; set; }
    }

    internal class Stats
    {

        [JsonProperty("sequence_mail")]
        public string SequenceMail { get; set; }

        [JsonProperty("created_addresses")]
        public int CreatedAddresses { get; set; }

        [JsonProperty("received_emails")]
        public string ReceivedEmails { get; set; }

        [JsonProperty("total")]
        public string Total { get; set; }

        [JsonProperty("total_per_hour")]
        public string TotalPerHour { get; set; }
    }

    internal class Auth
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error_codes")]
        public object[] ErrorCodes { get; set; }
    }

    internal class Letters
    {

        [JsonProperty("list")]
        public Letter[] List { get; set; }

        [JsonProperty("count")]
        public string Count { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("ts")]
        public int Ts { get; set; }

        [JsonProperty("sid_token")]
        public string SidToken { get; set; }

        [JsonProperty("stats")]
        public Stats Stats { get; set; }

        [JsonProperty("auth")]
        public Auth Auth { get; set; }
    }
}
