namespace Common;

public enum MessageType : byte
{
    JOIN,              // c->s
    START_GAME,        // s->c
    ARCHETYPE,         // c->s
    START_TURN,        // s->c
    PRODUCTION_RESULT, // s->c
    MAKE_SOLDIERS,     // c->s
    ATTACK,            // c->s
    ATTACK_TARGET,     // s->c (атакующему)
    ATTACK_RECEIVED,   // s->c (атакуемому)
    BUILD,             // c->s
    UPGRADE,           // c->s
    END_TURN,          // c->s
    TURN_ENDED,        // s->c
    STATE,             // s->c
    GAME_END,          // s->c
    RESPONSE,          // s->c
    PLAYER_LEFT        // s->c (игрок отключился)
}
