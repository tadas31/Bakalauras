﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    class Player
    {
        public int id;
        public string username;
        public Deck deck;

        public Player(int _id, string _username)
        {
            id = _id;
            username = _username;
        }
    }
}
