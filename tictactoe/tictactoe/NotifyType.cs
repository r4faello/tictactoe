using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tictactoe
{
    public enum NotifyType
    {
        Userexists,
        // Savo
        UserAlreadyExists,
        AccountSuccessfullyCreated,
        UserDoNotExist,
        SuccessfullyLoggedIn,
        SearchingForPlayers,
        GameStarted,
        XOPlaced,
        XOShow,
        Show,
        ShowWinningMsg,
        ShowLossMsg,
        HighlightButtons
    }
}
