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

        MoveAnimation _move;
        BreathAnimation _breath;
        
        public MoveAnimation Move => _move;
        public BreathAnimation Breath => _breath;
        void Create()
        {
            _move = new MoveAnimation(_model, _window);
            _breath = new BreathAnimation(_model, _window, _window);
        }
    }
}