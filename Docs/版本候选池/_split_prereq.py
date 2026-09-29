# -*- coding: utf-8 -*-
"""One-shot: add 需求前置条件 and split pool files. Deleted after use."""
from pathlib import Path

ROOT = Path(__file__).resolve().parent

# title -> (top, requirement_sentence, optional full 前置条件 replacement or None)
# top means both implementation and requirement start with 无
ITEMS = {}

def add(title, top, req, prereq=None):
    ITEMS[title] = (top, req, prereq)

# --- 五小时内开 26, all bottom ---
add("捡回死亡留下的灵魂", False, "无。FR-燃火-06 写交互后残留消失，灵魂增加记下的数量。")
add("没捡就再死，只留新的一笔", False, "无。FR-燃火-07 写旧残留消失，新的只等于这次身上的灵魂。")
add("中毒会持续掉血，紫苔能停", False, "持续掉血要维持多久，[状态积蓄](../需求文档/功能需求/状态积蓄.md) 写时长与调研待核对项一致，还没有确定的时长。")
add("剧毒不能用解中毒的那一瓶", False, "剧毒要掉多久，同一篇写时长仍待核对。哪一瓶能停，FR-异常-03 已写普通紫苔不停、开花紫苔才停。")
add("休息不取消余烬，死亡会取消", False, "无。FR-周目-02 写休息后上限仍是余烬那一档，死亡后熄灭。")
add("清除暗印之后不能窃火", False, "暗印要几枚才算窃火成立，FR-周目-07 写枚数门槛与调研待核对项一致，还没有确定的枚数。")
add("结局之后还能再进下一周", False, "无。留下什么、重来什么按 FR-周目-05 已经写明的那一份。敌人更难的幅度仍待核对，本条不选。")
add("武器技扣蓝，双手也有动作", False, "蓝不够时武器技出不出，还没合成。FR-攻击-06 写动作仍然播放、特殊效果变弱或没有；本条写成不播武器技、改播耸肩。精力不够时放不放见未入池，本条不选。")
add("交书之后清单变长，人死了清单关闭", False, "无。FR-任务-04 写交书后清单变长，商人死亡后打不开。")
add("商人死亡留下可以交还的灰烬", False, "无。FR-任务-06 写死亡留下灰烬，交还后可以再买。")
add("一条任务失败后休息不会重新打开", False, "无。FR-任务-07 写互斥的一条立起后另一条关掉，休息不重新打开。")
add("身上只有当前这枚印记在生效", False, "无。FR-联机-01 写只有当前装备的那一枚在改可见效果。")
add("上交到两档才领奖", False, "无。两档各给一次、同一档不重复，FR-联机-02 已写。下一周目是否再发见未入池，本条不选。件数放在资产上。")
add("入侵成功才拿到上交物", False, "无。FR-联机-05 写主人死亡才给这件，入侵者自己死亡则没有。")
add("炭火没交之前选不到那一组", False, "无。FR-注入-02 写炭火没交给铁匠时那一组选不到。")
add("普通强化停在加十，另两条停在加五", False, "无。FR-注入-03 写普通停在加十，鳞片和闪耀停在加五。")
add("王座用首领魂换走一项", False, "无。是否另收一笔灵魂见调研待核对，本条按需求不另收。只消耗这颗魂、得到一项，FR-注入-04 已写。")
add("使用暗印不留下可捡血迹", False, "无。FR-周目-03 写送回火边、灵魂为 0、使用点没有残留，头目血条出现时放不出去。")
add("咒死蓄满则死亡", False, "离开来源后槽往回掉的速度，[状态积蓄](../需求文档/功能需求/状态积蓄.md) 写与调研待核对项一致，还没有确定的速度。牺牲戒指是否无视咒死见调研待核对，本条不选。")
add("提高体力后翻滚变轻", False, "体力每一级增加多少负重上限，[角色与养成](../调研文档/角色与养成.md) 仍写精确曲线待核对。没有这条曲线，就不知道加到哪一级算跨档。")
add("有协助时头目更厚", False, "无。有协助时头目生命高于单人，FR-头目-05 已写这一眼。攻击欲望会不会改表见调研待核对，本条不选。")
add("背包按页分开", False, "无。不同种类不画在同一页，FR-菜单-02 已写。页签名是否和调研逐字相同见调研待核对，本条不纠结用词。")
add("无敌期间这一下不加异常槽", False, "无。无敌为真时槽不上升，FR-异常-06 已写。翻滚从第几帧打开无敌见未入池，本条不选。")
add("鳞片和闪耀路线不进十五种注入", False, "无。FR-注入-05 写这两种路线的菜单里没有那十五种。")
add(
    "穿上护甲后韧性提高",
    False,
    "四件护甲的韧性如何合成一个数，[装备与建造](../调研文档/装备与建造.md) 仍标待核对。合成没写明之前，不把护甲韧性写进结算。FR-硬直-05 写没开霸体窗口时穿甲仍播受击，本条写成穿上后受击动画不再出现，这一眼也还没对齐。",
    "无。武器霸体窗口已经会加上 `offensivePoiseBonus`。穿甲已会写五段吸收（0.11）。韧性到时写回已有（0.28）。",
)
add(
    "治疗随催化器威力增加",
    False,
    "即时治疗怎么乘这个威力，需求和调研还没写成同一条式子。回得更多、不超过上限，FR-法术-06 只写了方向。持续治疗的总量是否正比见未入池，本条不选。",
    "催化器上先有可读的法术威力。现在 `HealingSpell` 仍按固定 `healAmount` 加血。",
)

# --- 五小时内关 14 ---
add("终局一次交互只进入一条结局", True, "无。按身上旗标只进三条中的一条，FR-周目-06 已写窃火、灭火、传火各走哪条。")
add("坐下后的菜单是四项", True, "无。四项是传送、整理法术、储物箱、离开，没有升级，FR-篝火-01 已写。")
add("四种催化器各放各的系", True, "无。法杖、圣铃、护符、火焰各放各的系，拿错没有弹体或治疗，FR-法术-04 已写。")
add("从防火女打开加点", True, "无。够了才加一级并扣魂，篝火四项里没有升级，FR-养成-05 已写。每一级花多少灵魂见未入池，本条不写算式。")
add("装备槽能看到双手各三、四防具和四戒指", True, "无。每手三格、头胸手腿、戒指四格，FR-菜单-03 已写。")
add("商店用灵魂买卖", True, "无。买得到、卖得掉、灵魂会变、不够时两边不变，FR-任务-02 已写。卖出比例见未入池，本条不选。")
add("再注入覆盖旧的，加号留下", True, "无。换成另一种注入后加号还在，FR-注入-01 已写。")
add("传送只去已经点燃的火", False, "无。只能去已经点燃的火，FR-篝火-02 已写。")
add("各处火的储物箱是同一个", False, "无。各处火取回的是同一份物品，FR-篝火-03 已写。")
add("余烬时才能召协助", False, "无。没有余烬召不来，有余烬才召来，FR-联机-03 已写。")
add("冰霜蓄满后用火解开", False, "冰霜这层要持续多久，[状态积蓄](../需求文档/功能需求/状态积蓄.md) 写时长与调研待核对项一致，还没有确定的时长。刚结算后的空档多长仍见调研待核对，本条不选。")
add("法术记忆只在篝火改", False, "无。只有坐下后能改记忆栏，FR-法术-03 已写。同一法术再选一次是否加次数见未入池，本条不选。")
add("四枚戒指戴上就能看见变化", False, "无。最多四枚，戴上能看出该类型的变化，第五枚无处可戴，FR-注入-07 已写。同名各强化级能否同戴见调研待核对，本条不选。")
add("重生搬动点数，灵魂等级不动", False, "无。点数挪走、灵魂等级不变、不低于职业起点，FR-养成-06 已写。能搬几次见调研待核对，本条只做搬一次。")

# --- 高风险 五小时内开 ---
add("回城骨把人送回上次的火", False, "无。回到上次的火，灵魂不清零，余烬不熄灭，药瓶和周围敌人按休息重置，FR-周目-04 已写。")
add("协助打不中同伴，入侵打得中主人", False, "无。协助打不中同伴和主人，入侵打得中主人，FR-联机-04 已写。")
add("世界敌人默认不追入侵者", False, "无。普通敌人默认不追入侵者，FR-联机-09 已写。")

# --- 高风险 五小时内关 ---
add("呼救被打断则同伴不来", True, "无。呼救做完同伴才来，做完前被打断则不来，同组的人发现时就转过来，FR-头目-02 已写。")
add("头目换阶段", False, "无。降到门槛后换招式或血条，这段里可以打不中，FR-头目-04 已写。")
add("死后撤雾并点起新火", False, "无。死亡后雾门撤掉，本周目不再出现，可以点起新火，FR-头目-06 已写。")
add("一场头目的场地机关", False, "无。这场配置了的机关在战里生效，战后保持写明的开关，FR-头目-07 已写。哪一面墙是幻影墙不在本条。")
add("死亡时灵魂变成零并留下残留", True, "无。身上变为 0，残留等于死前灵魂，本来是 0 不生成，FR-燃火-05 已写。")
add("出血蓄满掉一截", True, "无。蓄满掉一截、槽清空、红苔清槽，FR-异常-01 已写。掉一截再加多少比例见调研待核对，本条不选。")
add("下一周目留下人，重来世界", False, "无。等级、装备、法术和药瓶强化留下，火、门、钥匙和头目战败按新的一周重来，FR-周目-05 已写。敌人更难的幅度仍待核对，本条不选。")
add("攻击后敌对，赦罪恢复活人的对话", False, "一击就敌对还是打到一定伤害，调研仍待核对。本条按需求写成攻击后敌对，两边还没合成。")
add("休息后已死的近战回到满血", False, "无。休息后回到原位且生命为满，没休息保持消失，FR-燃火-03 已写。")

# --- 人工关 五小时内开 ---
add("轻轻推摇杆会走", True, "无。输入较小时慢于跑步，推满仍是跑步，FR-移动-08 已写。30% 负重以下是否再快一档见未入池，本条不选。")
add("头目使用带名字的血条", True, "无。开战时出现带名字的长血条，长度对应当前生命，FR-头目-03 已写。")
add(
    "崩盾之后可以处决",
    True,
    "无。盾被打空并弯腰后贴上去进入处决，FR-处决-04 已写。",
    "无。格挡耗尽已经会破防（0.16）。",
)
add("身上出现锁定标记", True, "无。锁定成功时目标身上出现标记，死亡或解开后消失，FR-镜头-07 已写。")

# --- 弹反 ---
add(
    "弹开之后可以处决",
    False,
    "弹反窗口有多少帧，FR-处决-03 写与调研待核对项一致，还没有确定的帧数。",
    "无。弹反已经会把攻击者打成 `\"Parried\"`。",
)

# --- 四档翻滚 ---
add("四档翻滚能分辨，超重不能翻", False, "无。低于 30%、30% 与 70%、高于 70%、达到 100% 这四档，以及轻中 13 帧、重 12 帧，需求和调研已经写成同一句。窗口从第几帧开始、后撤有没有无敌，见未入池，本条不选。")

# --- 人工关 高风险 ---
add("追出距离后走回起点", True, "无。超过配置距离后停止追击，走回发现前的位置并回到待机，FR-敌人-05 已写。")
add("拉出活动范围后归位并补满", False, "无。拉出活动范围后走回放置点并回满，头目不因此回血，FR-头目-01 已写。")
add("走进雾门后退不出来", False, "无。交互后满血开战，没结束走不回去，死亡后再进仍是满血，FR-机关-05 已写。")
add("敌对的人会喝药和翻滚", False, "无。敌对人形会喝药和翻滚，次数和精力按它自己的配置下降，FR-头目-08 已写。")
add("背后贴住进入背刺", True, "无。背后距离内锁进背刺，掉一次血，播完才能再动，FR-处决-01 已写。伤害倍率和鞭类不进背刺见调研待核对，本条不选。")

# --- 美术关 五小时内开 ---
add("有钥匙才打得开的门", False, "无。没有钥匙打不开，有钥匙打开并保持，FR-机关-03 已写这一眼。钥匙用完消不消失见未入池，本条不选。")
add("野兽不进入背刺动画", False, "无。对野兽是普通一击，不锁背刺，FR-处决-05 已写。")
add("状态弹推进对应的槽", False, "无。只推进配置的那一种槽，FR-远程-05 已写。")

# --- 美术关 五小时内关 ---
add("改造只改外形", False, "无。外形变，九项和灵魂等级不变，FR-养成-07 已写。每个周目能改造几次见调研待核对，本条不选。")
add("只列出已经解锁的姿态", False, "无。只列出已解锁的条目，点一条能播，FR-菜单-06 已写。")
add("打开的门休息后仍开着", True, "无。打开后门保持打开，休息或死亡后再回来仍开着，FR-机关-02 已写。")
add("弓的拉满和快射能分辨", True, "无。拉满和快射的伤害能分辨，两档都扣一发弹，FR-远程-01 已写。")
add("弩和螺栓、大弓和大箭各用各的", False, "无。弩只用螺栓，大弓只用大箭，互相当弹药时没有飞行物，FR-远程-02 已写。")
add(
    "踢击崩开举盾",
    True,
    "无。没在冲刺时踢中举盾，对方精力被打空并进入崩架，FR-架势-05 已写。能否踢中超大武器的挥击见调研待核对，本条不选。",
    "无。举盾和破防已有（0.16）。",
)
add("留言和别人的血渍只在在线时出现", True, "无。在线才出现留言和别人的死亡标记，离线不出现，自己的灵魂残留不受影响，FR-联机-07 已写。")
add("离线时教堂枪兵由本地角色顶上", False, "无。离线时对手是场景里的本地角色，FR-联机-08 已写。")
add(
    "带着暗印死亡会加深空虚",
    False,
    "空虚增加多少、清除要花多少灵魂，FR-养成-08 写与调研待核对项一致，还没有确定的数。",
    "无。死亡流程已有。清除暗印用现有对话（1.5）。",
)
add("电梯可以再拉一次", True, "无。再拉一次回到刚才离开的方向，FR-机关-04 已写这一眼。休息后停在哪一层见未入池，本条不选。")
add(
    "武器附魔会在一段时间后结束",
    True,
    "无。允许附魔的武器在一段时间内多一段伤害，时间到或换武器后没有了；涂不上的名单 FR-注入-08 已写。失败是否仍扣蓝见未入池，本条不选。",
    "无。近战已经会结算五段伤害（1.0、1.46）。",
)

# --- singles ---
add("创角交出外形和赠礼", True, "无。创角结束后带着外形、名字、职业和一件葬礼赠礼，FR-养成-01 已写。赠礼完整名单仍待核对，本条不选哪一件。")
add(
    "成对武器打出双持连段",
    True,
    "无。成对武器打出双手连段、另一只手不能同时举真盾，两把普通武器不会合成，FR-架势-03 与 FR-架势-04 已写。成对武器的重量是否算两次见调研待核对，本条不选。",
    "无。单手轻击连段已有。",
)
add("架势的两段派生和收招", True, "无。按下后停在姿势里，再按轻击或重击走出两段，不接可以收回，FR-架势-01 与 FR-架势-02 已写。进入时扣不扣蓝见未入池，本条不选。")
add("登梯、滑下和向外跳", True, "无。能上下，按住闪避向下滑到底，摇杆回中再按闪避向梯外下落，FR-机关-01 已写。梯子上精力是快是停见未入池，本条不选。")
add("挥击中段的攻击霸体", True, "无。配了进攻霸体的那一段被普通攻击打中时挥击继续、血仍掉，结束后同一击会打出硬直，FR-硬直-04 已写。超级霸体见未入池，本条不选。")
add("拟态打开会咬，打中会站起来", False, "无。打开会被咬并掉血，先打中则直接站起来，本周目死后不再变回箱子，FR-机关-08 已写。")

# --- 1个以上 FR 五小时内关 ---
add(
    "换装后能读到负重",
    False,
    "装备或卸下箭、螺栓时负重变不变，还没合成。FR-装备-08 和调研「负重当位」写不计；[装备与建造](../调研文档/装备与建造.md)「弹药」仍写弹药叠有重量、算哪一叠待核对。负重上限随体力怎么涨，[角色与养成](../调研文档/角色与养成.md) 仍写曲线待核对，而 FR-负重-01 要能看见上限。",
)
add(
    "需求不足时更慢，打中也更弱",
    False,
    "无。力量或敏捷不足时仍能挥、伤害更低，FR-属性-05 已写；挥击更慢，FR-注入-06 已写。智力、信仰、幸运不足也变慢，是 FR-注入-06 里本条不选的其余几项。双手力量怎么取整见未入池，本条不选。",
    "武器伤害和挥击已有。`WeaponItem` 上还没有力量或敏捷的需求点数。",
)


def split_items(text):
    lines = text.splitlines()
    # keep newline style
    idx = next(i for i, line in enumerate(lines) if line.startswith("## "))
    header = lines[:idx]
    body = lines[idx:]
    items = []
    cur = None
    buf = []
    for line in body:
        if line.startswith("## "):
            if cur is not None:
                items.append((cur, buf))
            cur = line[3:].strip()
            buf = []
        else:
            buf.append(line)
    if cur is not None:
        items.append((cur, buf))
    return header, items


def rewrite(path: Path):
    raw = path.read_bytes()
    nl = "\r\n" if b"\r\n" in raw else "\n"
    text = raw.decode("utf-8")
    header, items = split_items(text)
    new_header = []
    for line in header:
        if line.startswith("本文件 "):
            continue
        if line.startswith("武器技扣蓝和咒死"):
            continue
        new_header.append(line)
    # drop trailing blanks; we'll add our own
    while new_header and new_header[-1] == "":
        new_header.pop()

    top, bot = [], []
    for title, buf in items:
        if title not in ITEMS:
            raise SystemExit(f"missing map: {path.name} :: {title}")
        is_top, req, prereq = ITEMS[title]
        # trim trailing blanks in item
        while buf and buf[-1] == "":
            buf.pop()
        out = []
        inserted = False
        for line in buf:
            if line.startswith("- 前置条件："):
                if prereq is not None:
                    line = "- 前置条件：" + prereq
                out.append(line)
                out.append("- 需求前置条件：" + req)
                inserted = True
            else:
                out.append(line)
        if not inserted:
            raise SystemExit(f"no 前置条件: {title}")
        pre_line = next(x for x in out if x.startswith("- 前置条件："))
        req_line = next(x for x in out if x.startswith("- 需求前置条件："))
        both = pre_line.startswith("- 前置条件：无") and req_line.startswith("- 需求前置条件：无")
        if both != is_top:
            raise SystemExit(f"section mismatch {title}: top={is_top} both={both}\n{pre_line}\n{req_line}")
        block = ["### " + title, *out, ""]
        (top if is_top else bot).append(block)

    n = len(items)
    new_header.append("")
    new_header.append(f"本文件 {n} 条。全都解锁 {len(top)} 条，还未解锁完 {len(bot)} 条。索引见 [README.md](README.md)。")
    new_header.append("")
    new_header.append("## 全都解锁")
    new_header.append("")
    if not top:
        new_header.append("这一段没有。")
        new_header.append("")
    new_header.append("## 还未解锁完")
    new_header.append("")
    if not bot:
        new_header.append("这一段没有。")
        new_header.append("")

    parts = [nl.join(new_header).rstrip("\r\n")]
    # The header currently has BOTH section titles before items. Need to insert items under the right heading.
    # Rebuild properly.
    head = []
    for line in header:
        if line.startswith("本文件 "):
            continue
        if line.startswith("武器技扣蓝和咒死"):
            continue
        head.append(line)
    while head and head[-1] == "":
        head.pop()
    head.append("")
    head.append(f"本文件 {n} 条。全都解锁 {len(top)} 条，还未解锁完 {len(bot)} 条。索引见 [README.md](README.md)。")
    head.append("")
    chunks = [nl.join(head)]
    chunks.append("## 全都解锁" + nl + nl + (("这一段没有。" + nl) if not top else "".join(nl.join(b) + nl for b in top)))
    chunks.append("## 还未解锁完" + nl + nl + (("这一段没有。" + nl) if not bot else "".join(nl.join(b) + nl for b in bot)))
    out = nl.join(chunks)
    if not out.endswith(nl):
        out += nl
    path.write_bytes(out.encode("utf-8"))
    return len(top), len(bot), n


def main():
    files = sorted(
        p for p in ROOT.glob("*.md")
        if p.name not in ("README.md", "未入池.md")
        and not p.name.endswith(("-1.md", "-2.md", "-3.md", "-4.md"))
        and "开或关" not in p.name
    )
    # only tracked canonical names: those without numeric suffix already filtered if we exclude -1 etc.
    # Also exclude this script's neighbors that are untracked duplicates: the glob of *.md in the folder includes untracked -1 files. Filtered above.
    used = set()
    for p in files:
        _, items = split_items(p.read_text(encoding="utf-8"))
        for title, _ in items:
            if title in used:
                raise SystemExit(f"dup title {title}")
            used.add(title)
    extra = set(ITEMS) - used
    missing = used - set(ITEMS)
    if extra or missing:
        (ROOT / "_mismatch.txt").write_text(
            "EXTRA\n" + "\n".join(sorted(extra)) + "\n\nMISSING\n" + "\n".join(sorted(missing)),
            encoding="utf-8",
        )
        raise SystemExit(f"extra={len(extra)} missing={len(missing)} see _mismatch.txt")
    for p in files:
        a, b, n = rewrite(p)
        print(f"{a}+{b}={n} {p.name}")
    print("files", len(files))


if __name__ == "__main__":
    main()
