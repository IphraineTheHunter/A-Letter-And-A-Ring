namespace Assets.Scripts.Combat
{
    public class Injury
    {
        public int Severity = 0;
        public BodyParts location = BodyParts.Eyes;
    }

    public enum BodyParts {Eyes, Ears, Brain, RightArm, RightHand, LeftArm, LeftHand, Torso, Legs};
}