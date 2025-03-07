using CIS_106_ASSIGNMENT_2.models;

namespace CIS_106_ASSIGNMENT_2.services
{

    static class ProgressReportGenerator
    {

        public static void GenerateProgressReport(List<Character> characters, List<Level> levels)
        {
            string level_prefix = "--";
            string powerup_prefix = "----";
            foreach (Character character in characters)
            {
                Console.WriteLine($"Progress Report for {character.Name}");
                foreach (Level level in levels)
                {
                    Console.WriteLine("{0}{1} Power Ups Collected:",
                    level_prefix,
                    level.Name);
                    foreach (PowerUp powerUp in level.PowerUps)
                    {
                        Console.WriteLine("{0}{1,20}:{2,4}",
                        powerup_prefix,
                        powerUp.Name,
                        character.PowerUps.Contains(powerUp) ? "[X]" : "[ ]"
                        );
                    }
                }
            }
        }
    }

}