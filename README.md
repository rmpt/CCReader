# CCReader
Biblioteca para ler dados do cartão de cidadão

Classe principal: **CCReaderAPI**


#### Notas
- A leitura das imagens é feita com a lib CSJ2K pois as imagens estão guardados no cartão em formato JPEG2000.
- O Middleware do cartão de cidadão deve estar instalado. Download pode ser feito **[aqui](https://1drv.ms/f/s!AkLsBHidk5AH2jVcrqr-xsTvuJpu "aqui")**


**Exemplo**

Utilizado as DLL da ultima release, a leitura dos dados pode ser feita da seguinte forma:

	var ccApi = new CCReaderAPI();
	
	Citizen citizen;
	try
	{
		citizen = ccApi.Read();
	}
	catch (PteidException e)
	{
		HandlePteidException(e);
		return;
	}
	// O objecto citizen tem todos os dados do cartão
