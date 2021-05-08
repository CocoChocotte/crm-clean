namespace CRM.Application.Impot.Results.CollectImpot
{
    public class CollectImpotCommand
    {
        //https://www.journaldunet.fr/patrimoine/guide-des-finances-personnelles/1151203-impot-sur-le-revenu-2021-combien-vous-paierez-cette-annee/
        //Barrème 2021
        private const int Tranche1 = 10085;
        private const int Tranche2 = 25710;
        private const int Tranche3 = 73516;
        private const int Tranche4 = 158122;

        private const int Taux1 = 0;
        private const float Taux2 = 0.11f;
        private const float Taux3 = 0.3f;
        private const float Taux4 = 0.41f;
        private const float Taux5 = 0.45f;


        public float GetImpot(float nbPart, float revenu)
        {
            var revenuImposable = revenu / nbPart;

            if (revenuImposable < Tranche1)
                return revenuImposable * Taux1; // =  0

            if (revenuImposable > Tranche1 && revenuImposable < Tranche2)
                return ((revenuImposable * Taux1) + (revenuImposable - Tranche1) * Taux2) * nbPart;

            if (revenuImposable > Tranche2 && revenuImposable < Tranche3)
                return ((revenuImposable * Taux1) + (Tranche2 - Tranche1) * Taux2 +
                        (revenuImposable - Tranche2) * Taux3) * nbPart;

            if (revenuImposable > Tranche3 && revenuImposable < Tranche4)
                return (revenuImposable * Taux1 + (Tranche2 - Tranche1) * Taux2 + (Tranche3 - Tranche2) * Taux3 +
                        (revenuImposable - Tranche3) * Taux4) * nbPart;

            return (revenuImposable * Taux1 + (Tranche2 - Tranche1) * Taux2 + (Tranche3 - Tranche2) * Taux3 +
                    (Tranche4 - Tranche3) * Taux4 + (revenuImposable - Tranche4) * Taux5) * nbPart;
        }
    }
}