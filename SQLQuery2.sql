USE BDVENTAS2022eco
GO

CREATE OR ALTER PROC PA_LISTAR_ARTICULOS
AS
SELECT A.nom_art, pre_art,cod_art, 
       A.stk_art
FROM Articulos A
GO

--exec PA_LISTAR_ARTICULOS
--go

CREATE OR ALTER PROCEDURE PA_LISTAR_ARTICULOS_POR_NOMBRE
    @nombreInicial NVARCHAR(50)
AS
BEGIN
    -- Evita que errores de sintaxis detengan la ejecución
    SET NOCOUNT ON;

    SELECT 
        A.nom_art,
        A.pre_art,
        A.cod_art,
        A.stk_art
    FROM 
        Articulos A
    WHERE 
        A.nom_art LIKE @nombreInicial + '%'
END
GO


EXEC PA_LISTAR_ARTICULOS_POR_NOMBRE @nombreInicial = 'MO';

