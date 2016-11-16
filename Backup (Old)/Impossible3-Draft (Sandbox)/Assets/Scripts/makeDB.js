#pragma strict
// With help from: http://wiki.unity3d.com/index.php/SQLite

public var DatabaseName : String = "TestDB.sqdb";
public var enemies : String = "enemies";
public var enemyTable : Array = ["ID", "type", "description", "health", "attTime", "attPow", "mvTime", "mvDist", "armVal"];
public var enemyColumns : Array = ["text", "text", "text", "real", "real", "real", "real", "real", "real"];

public var characters : String = "characters";
var db : dbAccess;

function Start () {
    
    db = new dbAccess();
    db.OpenDB(DatabaseName);
    
    /*var query = db.BasicQuery("SELECT name FROM sqlite_master WHERE type='table' AND name='" + enemies + "'", true);
    var table : ArrayList = new ArrayList();
    while (query.Read()) {
        var row : ArrayList = new ArrayList();
        for (var i:int = 0; i < query.FieldCount; i++) {
            row.Add(query.GetValue(i));
        }
        table.Add(row);
    }
    
    for (var k:int = 0; k < table.Count; k++) {
        var slice : ArrayList = table[k];
        for (var j:int = 0; j < slice.Count; j++) {
            Debug.Log(slice[j]);
            if (slice[j] === enemies) {
                //Ugh.
            }
        }
    }*/
    
    try {
        
        //db.BasicQuery("DROP TABLE IF EXISTS " + enemies, false);
        db.CreateTable(enemies, enemyTable, enemyColumns);
        
        db.InsertInto(enemies, new Array("c4ee765c5f44400ba8ae7c0d87947dff", "goblin", "So many of them, this place crawls!", 20.0, 0.0, 20.0, 0.0, 1.0, 5.0));
        db.InsertInto(enemies, new Array("ec848d74841d498d990942c135ff9c81", "skeleton", "Buried underground, coming up for air, MUST, EAT, FLESH.", 35.0, 0.0, 0.0, 0.0, 1.0, 0.0));
        db.InsertInto(enemies, new Array("ec4ca9d82e464daeac53de2ab166f5e4", "kobucha", "From the dark underbelly of your parents' garage.", 10.0, 0.0, 10.0, 0.0, 1.0, 10.0));
        db.InsertInto(enemies, new Array("3111bfa2b9964c189d773a04060c93a3", "skeleton-melee", "BRAWL BRAWL BRAWL!", 35.0, 0.0, 20.0, 0.0, 1.0, 10.0));
        db.InsertInto(enemies, new Array("cb1e3f95d0af4a12bd7bde2828992641", "skeleton-range", "A veritable rain of arrows.", 35.0, 0.0, 10.0, 0.0, 1.0, 10.0));
        
    } catch (e) {
        Debug.Log(e);
    }

}