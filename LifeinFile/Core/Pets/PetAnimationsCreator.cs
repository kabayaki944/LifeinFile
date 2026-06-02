using LifeinFile.Models.Pets;
using LifeinFile.Windows;

namespace LifeinFile.Views.Pets
{
    public class PetAnimationsCreator
    {
        PetModel _model;
        PetWindow _window;
        public PetAnimationsCreator(PetModel model, PetWindow window)
        {
            _model = model;
            _window = window;
            
            Create();
        }

        MovePetAnimation _movePet;
        BreathPetAnimation _breathPet;
        
        public MovePetAnimation MovePet => _movePet;
        public BreathPetAnimation BreathPet => _breathPet;
        void Create()
        {
            _movePet = new MovePetAnimation(_model, _window);
            _breathPet = new BreathPetAnimation(_model, _window, _window);
        }
    }
}