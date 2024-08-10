using System.Collections.Generic;
using System.Numerics;

namespace HuntKit.Spawnpoints;

public static class Spawnpoints
{
    static readonly List<Vector3> Lakeland = [
        new Vector3(275, 22, 803),
        new Vector3(313, 22, 457),
        new Vector3(85, 12, 421),
        new Vector3(697, 22, 539),
        new Vector3(725, 22, 271),
        new Vector3(476, 22, 40),
        new Vector3(208, 22, 119),
        new Vector3(-149, 22, 78),
        new Vector3(-368, 22, 163),
        new Vector3(-670, 97, 73),
        new Vector3(-491, 32, -209),
        new Vector3(-483, 81, -430),
        new Vector3(-78, 97, -591),
        new Vector3(84, 116, -453),
        new Vector3(308, 121, -294),
        new Vector3(407, 125, -114),
        new Vector3(700, 39, -274),
        new Vector3(767, 90, -453),
    ];
    static readonly List<Vector3> Thavnair = [
        new Vector3(-46,35,497),
        new Vector3(320,25,202),
        new Vector3(-152,50,113),
        new Vector3(270,33,-22),
        new Vector3(562,34,-68),
        new Vector3(407,31,-382),
        new Vector3(-175,103,-250),
        new Vector3(-160,100,-488),
        new Vector3(-350,90,-463)
    ];
    static readonly List<Vector3> Garlemald = [
        new Vector3(-276, 32, -87),
        new Vector3(-481, 38, -209),
        new Vector3(-470, 38, -427),
        new Vector3(-578, 41, -491),
        new Vector3(93, 9, 196),
        new Vector3(390, 26, -29),
        new Vector3(578, 29, 23),
        new Vector3(555, -2, 561),
        new Vector3(312, -2, 633),
    ];
    static readonly List<Vector3> Labyrinthos = [
        new Vector3(426, 195, -660),
        new Vector3(-219, 111, -591),
        new Vector3(-252, -6, -216),
        new Vector3(-536, 7, -106),
        new Vector3(-777, 6, 610),
        new Vector3(-467, -4, 697),
        new Vector3(-93, 0, 858),
        new Vector3(201, 0, 180),
        new Vector3(547, 97, 223),
        new Vector3(633, 187, -395),
    ];
    static readonly List<Vector3> MareLamentorum = [
        new Vector3(-547, 157, 131),
        new Vector3(-247, 89, 369),
        new Vector3(-206, 97, 169),
        new Vector3(-137, 93, 12),
        new Vector3(-27, 102, 656),
        new Vector3(140, 91, 603),
        new Vector3(136, 66, 104),
        new Vector3(341, 81, 271),
        new Vector3(428, 123, 430),
        new Vector3(756, 167, 286),
    ];
    static readonly List<Vector3> Elpis = [
        new Vector3(414, 1, 295),
        new Vector3(-139, -4, 152),
        new Vector3(-170, -30, 440),
        new Vector3(-425, -33, 540),
        new Vector3(-726, -33, 379),
        new Vector3(563, 148, -140),
        new Vector3(642, 148, -363),
        new Vector3(-4, 144, -404),
        new Vector3(14, 144, -763),
        new Vector3(-437, 315, -580),
    ];
    static readonly List<Vector3> UltimaThule = [
        new Vector3(-322, 84, 737),
        new Vector3(-10, 78, 642),
        new Vector3(-191, 78, 444),
        new Vector3(-258, 93, 238),
        new Vector3(-473, 70, 23),
        new Vector3(-657, 82, -54),
        new Vector3(-402, 273, -557),
        new Vector3(-103, 292, -592),
        new Vector3(332, 293, -454),
    ];
    static readonly List<Vector3> Urqopacha = [
        new Vector3(364, -133, -605),
        new Vector3(-123, -97, -368),
        new Vector3(-501, 0, -613),
        new Vector3(-680, 75, 201),
        new Vector3(-283, 84, 132),
        new Vector3(10, 44, -48),
        new Vector3(338, 70, 51),
    ];
    static readonly List<Vector3> Kozamauka = [
        new Vector3(-611, 11, -678),
        new Vector3(-740, 7, -478),
        new Vector3(-252, 8, -211),
        new Vector3(749, 20, -201),
        new Vector3(-809, 149, 365),
        new Vector3(-281, 125, 100),
        new Vector3(-55, 125, 347),
        new Vector3(127, 125, 760),
    ];
    static readonly List<Vector3> YakTel = [
        new Vector3(165, -134, 585),
        new Vector3(0, -134, 753),
        new Vector3(22, -155, 349),
        new Vector3(590, 26, -261),
        new Vector3(99, 26, -364),
        new Vector3(249, 26, -594),
        new Vector3(-646, 29, -98),
    ];
    static readonly List<Vector3> Shaaloani = [
        new Vector3(17, 8, 601),
        new Vector3(36, 8, 339),
        new Vector3(189, 8, 92),
        new Vector3(498, 8, 94),
        new Vector3(85, 8, -128),
        new Vector3(-619, 36, -243),
        new Vector3(-268, 43, -658),
        new Vector3(649, 8, -745),
    ];
    static readonly List<Vector3> HeritageFound = [
        new Vector3(410, 146, 422),
        new Vector3(303, 113, 615),
        new Vector3(410, 113, -94),
        new Vector3(140, 113, -87),
        new Vector3(292, 113, -381),
        new Vector3(-340, 33, 227),
        new Vector3(-659, 14, 601),
    ];
    static readonly List<Vector3> LivingMemory = [

    ];
    public static Dictionary<string,List<Vector3>> SpawnpointsDictionary = new Dictionary<string, List<Vector3>>() {
        // Endwalker
        {"Thavnair", Thavnair },
        {"Garlemald", Garlemald },
        {"Labyrinthos", Labyrinthos },
        {"MareLamentorum", MareLamentorum },
        {"Elpis", Elpis },
        {"UltimaThule", UltimaThule },
        // Dawntrail
        {"Urqopacha", Urqopacha },
        {"Kozamauka", Kozamauka },
        {"Shaaloani", Shaaloani },
        {"YakTel", YakTel },
        {"HeritageFound", HeritageFound },
        {"LivingMemory", LivingMemory },
        //Shadowbringers
    };
}
