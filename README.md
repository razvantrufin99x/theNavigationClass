# theNavigationClass
A tiny Forms and MSSQL navigation class for MSSQL databases writed in C#


Romanian 
Linia de cod   care permite folosirea clasei theNavigationClass este urmatoarea si se gaseste in Form1.cs

 nc = new theNavigationClass(this, "C:\\___\\srcmai2020B\\sqlServerConnectionTest\\sqlServerConnectionTest\\dbMDFTestDatabase.mdf", "clienti", "RUNALL"); 

Pentru a te conecta la o baza de date si a citi toate informatiile din ea fara efortul de a scrie un alt cod trebuie sa folositi in Form1.cs  linia 

  public theNavigationClass nc;

care creaza un obiect theNavigationClass si apoi the apelati contructorul new NavigationClass care are forma (forma_afisarii, calea bazei de date locale, denumirea tabelului,  RUNALL  care comanda functionarea clasei de navigare in baza de date astfel incat sa functioneze singura si sa ruleze toate comenzile.
