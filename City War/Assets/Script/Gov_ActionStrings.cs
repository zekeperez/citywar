using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gov_ActionStrings
{
    string lineBreakGap = "\n\n\n";

    public Gov_ActionStrings()
    {

    }

    public string[] getActionString(int index)
    {
        string[] returnString = new string[2];

        switch (index)
        {
            case 0: //bomb
            default:
                returnString[0] = "Bomb";
                returnString[1] = "The bomb is a lethal explosive device capable of fully destroying buildings.\n" +
                                                            "When used, anyone living inside will cease to exist, including the\n" +
                                                            "functionalities of the building." + lineBreakGap +

                                                            "+ Destroying the final stronghold can net you the win. \n" +
                                                            "= Destroying captured buildings won't have any repercussions, however \n" +
                                                            "the citizens might think otherwise.\n" +
                                                            "- Destroying innocent buildings will have repercussions.";
                return returnString;

            case 1: //drone
                returnString[0] = "Drone";
                returnString[1] = "Provides a surveillance drone to the target building.\n" +
                                                            "Reveals the current status of the building." + lineBreakGap +

                                                            "+ Reveals the real status of the building on that current turn.\n" +
                                                            "+ Citizens will gradually become unhappy due to its invasive nature.";

                return returnString;

            case 2: //shield
                returnString[0] = "Defensive Post";
                returnString[1] = "Send troops to defend a building. Prevents any form of capture.\n" +
                                                            "However, their presence will definitely displease the tenants." + lineBreakGap +

                                                            "+ Prevents the Terrorist from capturing the building once.\n" +
                                                            "- Reduce overall happiness.";
                return returnString;

            case 3: //barge
                returnString[0] = "Barge In";
                returnString[1] = "Send troops to barge into a building with lethal force authorized.\n" +
                                                            "Well - trained troops can decipher situations better than untrained troops." + lineBreakGap +


                                                            "+ Captured buildings will return to a neutral state.\n" +
                                                            "- Barging in to innocent buildings will have repercussions.\n" +
                                                            "-Strongholds can fend off barge in attacks.\n" +
                                                             "- Untrained officers may cause casualties.";
                return returnString;

            case 4: //police
                returnString[0] = "Patrol Check";
                returnString[1] = "Send troops to check in on houses. Depending on population happiness,\n" +
                                                            "civilians have the right to decline. Definitely cheaper and less intrusive\n" +
                                                            "than the drone." + lineBreakGap +

                                                            "= Only reveals if the building is currently occupied or not.\n" +
                                                            "= Chances of reveal depends on population happiness.";

                return returnString;

            case 5: //repair
                returnString[0] = "Building Reparations";
                returnString[1] = "Hire a repair team and rebuild the destroyed building. The building will\n" +
                                                            "immediately be available for use." + lineBreakGap +

                                                            "+ Repair a building.\n" +
                                                            "+ Increases population happiness.\n" +
                                                            "-It will instantly be available for use for the next player.";

                return returnString;
        }
    }
}
