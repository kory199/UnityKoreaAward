using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APIModels
{
    public class AccountRequest
    {
        public string ID { get; set; }
        public string Password { get; set; }
    }

    public class GameData
    {
        public string ID { get; set; }
        public string AuthToken { get; set; }
    }

    public class PlayerData
    {
        public long player_uid { get; set; }
        public int exp { get; set; }
        public int hp { get; set; }
        public int score { get; set; }
        public int level { get; set; }
        public int status { get; set; }
    }
}